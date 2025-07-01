using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pps_api.Managers;
using pps_api.Models;
using pps_api.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value);
            var username = User.Claims
                .Where(c => c.Type == JwtRegisteredClaimNames.Name)
                .Select(c => c.Value);

            return Ok(new
            {
                username,
                roles
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

        // Think of this as a logout endpoint.
        [HttpDelete]
        public IActionResult Delete(LoginRequest loginRequest)
        {
            var jti = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            var exp = User.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;

            if (jti != null && long.TryParse(exp, out var expUnix))
            {
                var expTime = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
                TokenBlacklistService.RevokeToken(jti, expTime);
            }

            return Ok(new { message = "Logged out successfully." });


            //try
            //{
            //    var jwt = Request.Headers.Authorization.ToString();
            //    if (!jwt.IsNullOrEmpty() && jwt.StartsWith("Bearer "))
            //    {
            //        loginRequest.Jwt = jwt.Substring("Bearer ".Length).Trim();
            //    }
            //    Response.Cookies.Delete("refreshToken", new CookieOptions
            //    {
            //        HttpOnly = true,
            //        Secure = true,
            //        SameSite = SameSiteMode.Strict
            //    });
            //    return Ok(Manager.DeleteUserSession(loginRequest));
            //}
            //catch (Exception ex)
            //{
            //    // Log the exception (ex) as needed
            //    return BadRequest(new {message = ex.Message });
            //}
        }
    }
}
