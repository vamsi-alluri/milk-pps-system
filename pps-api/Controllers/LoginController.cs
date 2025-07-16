using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pps_api.Managers;
using pps_api.Models;
using pps_api.Services;
using System.IdentityModel.Tokens.Jwt;

namespace pps_api.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly ILoginManager Manager;
        private readonly ITokenBlacklistService TokenBlacklistService;

        public LoginController(ILoginManager manager, ITokenBlacklistService tokenBlacklistService)
        {
            Manager = manager;
            TokenBlacklistService = tokenBlacklistService;
        }

        private string getUserInfoFromJwt(string token_str)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token_str);

            var username = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value ?? "";
            var accessScopesJson = jwtToken.Claims.FirstOrDefault(c => c.Type == "AccessScopes")?.Value ?? "[]";

            var userInfo = new
            {
                username,
                accessScope = System.Text.Json.JsonSerializer.Deserialize<List<AccessScope>>(accessScopesJson) ?? new()
            };

            return System.Text.Json.JsonSerializer.Serialize(userInfo);
        }

        // POST api/login
        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest login_request)
        {
            try
            {
                if (Manager.AuthenticateUser(login_request, out string? token_str, out DateTime expirationDate) && token_str != null)
                {
                    if (login_request.Jwt != null)
                    {
                        return Ok(); // If it is a JWT login, just return OK.
                    }
                    else
                    {
                        HttpContext.Response.Cookies.Append("jwt", token_str, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = expirationDate
                        });

                        HttpContext.Response.Cookies.Append("userInfo", getUserInfoFromJwt(token_str), new CookieOptions
                        {
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = expirationDate
                        });

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                return BadRequest(new { message = ex.Message });
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpGet("userinfo")]
        public IActionResult GetUserInfo()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value ?? "";

            // Extract the access scopes from the claim
            var accessScopesJson = User.Claims.FirstOrDefault(c => c.Type == "AccessScopes")?.Value;

            // Deserialize the access scopes JSON string into a list of objects
            List<AccessScope>? accessScopes = null;
            if (!string.IsNullOrEmpty(accessScopesJson))
            {
                accessScopes = System.Text.Json.JsonSerializer.Deserialize<List<AccessScope>>(accessScopesJson);
            }

            return Ok(new
            {
                username,
                accessScope = accessScopes ?? new List<AccessScope>()
            });
        }

        // This endpoint is for listing all logins, if needed.
#if DEBUG
        [HttpGet]
        public IActionResult ListLogins()
        {
            return Ok(Manager.ListLogins());
        }
#endif

        // Logout
        [HttpDelete]
        [Authorize]
        public IActionResult Delete()
        {
            var jti = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            var exp = User.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;

            if (jti != null && long.TryParse(exp, out var expUnix))
            {
                var expTime = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
                TokenBlacklistService.RevokeToken(jti, expTime);
            }

            HttpContext.Response.Cookies.Delete("jwt");
            HttpContext.Response.Cookies.Delete("userInfo");

            return Ok(new { message = "Logged out successfully." });
        }
    }
}
