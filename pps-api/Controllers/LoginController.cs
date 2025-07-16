using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using pps_api.Authorization;
using pps_api.Constants;
using pps_api.Managers.Interfaces;
using pps_api.Models;
using pps_api.Services;
using System.IdentityModel.Tokens.Jwt;
using pps_api.Enums;
using static pps_api.Constants.Constants;

namespace pps_api.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

        private readonly IUserManagementManager Manager;
        private readonly ITokenBlacklistService TokenBlacklistService;
        private readonly ITokenService TokenService;

        public LoginController(IUserManagementManager manager, ITokenBlacklistService tokenBlacklistService, ITokenService tokenService)
        {
            Manager = manager;
            TokenBlacklistService = tokenBlacklistService;
            TokenService = tokenService;
        }

        private string getUserInfoFromJwt(string token_str)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token_str);

            var username = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value ?? "";
            var accessScopesJson = jwtToken.Claims.FirstOrDefault(c => c.Type == ACCESS_SCOPES_STR)?.Value ?? "[]";

            var userInfo = new
            {
                username,
                accessScope = System.Text.Json.JsonSerializer.Deserialize<List<AccessScope>>(accessScopesJson) ?? new()
            };

            return System.Text.Json.JsonSerializer.Serialize(userInfo);
        }

        // POST api/login
        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest? login_request)
        {
            try
            {
                var username = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value ?? "";

                if (string.IsNullOrEmpty(username) == false && login_request?.Creds == null)
                {
                    // This will be here if the jwt is valid.
                    var jti = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
                    if (jti != null)
                    {
                        if (TokenBlacklistService.IsTokenRevoked(jti))
                        {
                            return Unauthorized(new
                            {
                                error = "Token has been revoked. Please log in again."
                            });
                        }
                        else
                        {
                            // Extract the access scopes from the claim
                            var accessScopesJson = User.Claims.FirstOrDefault(c => c.Type == ACCESS_SCOPES_STR)?.Value;

                            var userInfo = new
                            {
                                username,
                                accessScopes = System.Text.Json.JsonSerializer.Deserialize<List<AccessScope>>(accessScopesJson ?? "") ?? new()
                            };

                            var userInfoJson = System.Text.Json.JsonSerializer.Serialize(userInfo);

                            HttpContext.Response.Cookies.Append(USERINFO_STR, userInfoJson, new CookieOptions
                            {
                                Secure = true,
                                SameSite = SameSiteMode.Strict,
                                Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                            });
                            return Ok(new
                            {
                                message = "Success."
                            });
                        }
                    }
                }
                else
                {
                    // Creds provided, if jwt is provided, make sure to revoke it.
                    var jti = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
                    if (jti != null)
                    {
                        var exp = User.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;
                        if (exp != null && long.TryParse(exp, out var expUnix))
                        {
                            var expTime = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
                            TokenBlacklistService.RevokeToken(jti, expTime);
                        }
                    }

                    if (Manager.AuthenticateUser(login_request, out string? token_str, out DateTime expirationDate) && token_str != null)
                    {
                        HttpContext.Response.Cookies.Append(JWT_STR, token_str, new CookieOptions
                        {
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = expirationDate
                        });

                        HttpContext.Response.Cookies.Append(USERINFO_STR, getUserInfoFromJwt(token_str), new CookieOptions
                        {
                            Secure = true,
                            SameSite = SameSiteMode.Strict,
                            Expires = DateTimeOffset.UtcNow.AddMinutes(10)
                        });

                        return Ok();
                    }
                }
            }
            catch (Exception ex)
            {
                return Ok(new
                {
                    error = "An error occurred during authentication.",
                    details = ex.Message
                });
            }

            return Unauthorized();
        }

        // Logout: DELETE api/login
        [HttpDelete]
        [Authorize]
        public IActionResult Delete()
        {
            var username = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value;
            var jti = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
            var exp = User.FindFirst(JwtRegisteredClaimNames.Exp)?.Value;

            if (string.IsNullOrEmpty(username) == false && jti != null && long.TryParse(exp, out var expUnix))
            {
                var expTime = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
                TokenBlacklistService.RevokeToken(jti, expTime);
                TokenService.RemoveRefreshToken(username);
            }

            HttpContext.Response.Cookies.Delete(JWT_STR);
            HttpContext.Response.Cookies.Delete(USERINFO_STR);

            return Ok(new { message = "Logged out successfully." });
        }

        // Listing all active jwts from memory.
#if DEBUG
        [HttpGet]
        [RequireAccessScope(Departments.Admin, Roles.SuperAdmin)]
        public IActionResult ListLogins()
        {
            return Ok(Manager.ListLogins());
        }
#endif
    }
}
