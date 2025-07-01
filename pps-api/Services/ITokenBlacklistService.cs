using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace pps_api.Services
{
    public interface ITokenBlacklistService
    {
        public void RevokeToken(string jti, DateTime expiration);
        public bool IsTokenRevoked(string jti);
    }
}