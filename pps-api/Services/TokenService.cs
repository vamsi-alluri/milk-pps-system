using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using pps_api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace pps_api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _config;
        private readonly RandomNumberGenerator rng = RandomNumberGenerator.Create();
        private readonly JwtSecurityTokenHandler tokenHandler;

        public TokenService(IConfiguration config, IMemoryCache memoryCache)
        {
            _config = config;
            _memoryCache = memoryCache;
            tokenHandler = new JwtSecurityTokenHandler();
        }

        private IEnumerable<Claim> CreateClaims(string username = "Default", string role = "User")
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };
        }

        public bool ValidateToken(string token, out JwtSecurityToken? decodedJwtToken)
        {
            decodedJwtToken = null;
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                ValidateIssuer = true,
                ValidIssuer = _config["Jwt:Issuer"],
                ValidateAudience = true,
                ValidAudience = _config["Jwt:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero // Disable clock skew
            };
            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out SecurityToken decodedToken);
                decodedJwtToken = decodedToken as JwtSecurityToken;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool VerifyToken(JwtSecurityToken? jwtToken)
        {
            if (jwtToken == null)
            {
                return false;
            }

            if (jwtToken.ValidTo < DateTime.UtcNow)
            {
                // Token is expired - log it.
                return false;
            }

            if (jwtToken.Claims == null || !jwtToken.Claims.Any())
            {
                return false;
            }
            // Here you can add additional checks, e.g., checking roles or specific claims
            return true;
        }

        public string GenerateJwtToken(UserAuthInfo userAuthInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, userAuthInfo.UserId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("AccessScopes", JsonSerializer.Serialize(userAuthInfo.AccessScopes))
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: userAuthInfo.ExpirationDate,
                signingCredentials: creds
            );

            return tokenHandler.WriteToken(token);
        }


        private string? GetUserIdFromToken(JwtSecurityToken token)
        {
            return token?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        #region // Memory cache handling section.

        // This method checks if a token exists in the memory cache.
        public bool VerifyStoredToken(string? userId, string? token)
        {
            if (!userId.IsNullOrEmpty() && !token.IsNullOrEmpty())
            {
                bool _found = _memoryCache.TryGetValue($"token_{userId}", out string? out_token);
                return _found && string.Equals(token, out_token, StringComparison.OrdinalIgnoreCase);
            }
            return false;
        }

        private void StoreToken(string userId, DateTimeOffset expires, string? refreshToken = null)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(expires);

            _memoryCache.Set(               // Overwrites the previous token if it exists. The user can have only one active token(session) at a time.
                key: $"token_{userId}",
                value: refreshToken,
                options: cacheEntryOptions
            );
        }

        public string? GetAllRefreshTokens()
        {
            var tokens = (_memoryCache as MemoryCache)?.Keys.ToList();
            if (!tokens.IsNullOrEmpty() && tokens.Any())
            {
                return string.Join(", ", tokens);
            }
            return null;
        }

        public void RemoveRefreshToken(string userId)
        {
            _memoryCache.Remove($"token_{userId}");
        }
        #endregion


        [Obsolete("This method will be removed. It was intended to refresh the token based on a previous valid token, moving away from this approach.")]
        public string RefreshToken(string token_str, DateTime expires)
        {
            if (expires < DateTime.UtcNow)
            {
                throw new UnauthorizedAccessException("Token has expired, please login with credentials.");
            }

            if (!ValidateToken(token_str, out JwtSecurityToken? decodedJwtToken) && decodedJwtToken == null)
            {
                throw new UnauthorizedAccessException("Invalid token.");
            }

            if (decodedJwtToken == null || !VerifyToken(decodedJwtToken))
            {
                throw new UnauthorizedAccessException("Token verification failed.");
            }

            var userId = GetUserIdFromToken(decodedJwtToken);

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("UserId cannot be null or empty.");
            }
            var userAuthInfo = new UserAuthInfo
            {
                UserId = userId,
                AccessScopes = JsonSerializer.Deserialize<List<AccessScope>>(decodedJwtToken.Claims.FirstOrDefault(c => c.Type == "AccessScopes")?.Value ?? string.Empty) ?? new List<AccessScope>(),
                ExpirationDate = expires
            };

            var newToken = GenerateJwtToken(userAuthInfo);
            StoreToken(userId, expires, newToken);
            return newToken;
        }
    }

}
