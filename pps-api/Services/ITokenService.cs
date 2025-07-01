using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace pps_api.Services
{
    public interface ITokenService
    {
        public bool ValidateToken(string token, out JwtSecurityToken? decodedJwtToken);
        public bool VerifyToken(JwtSecurityToken? token);
        public bool VerifyStoredToken(string? userId, string? token);
        public string GenerateJwtToken(string userId, string role, DateTime expirationDate, IEnumerable<Claim>? claims = null);
        public void RemoveRefreshToken(string userId);
        public string? GetAllRefreshTokens();
        
        [Obsolete("Depending on future decisions, this method will be removed. It was intended to refresh the token based on a previous valid token, moving away from this approach.")]
        public string RefreshToken(string token, DateTime expires);

    }
}