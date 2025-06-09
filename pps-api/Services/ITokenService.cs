using System.Security.Claims;

namespace pps_api.Services
{
    public interface ITokenService
    {
        string GenerateJwtToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        void StoreRefreshToken(string userId, string refreshToken, DateTime expires);
    }
}