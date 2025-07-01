using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace pps_api.Services
{
    public class TokenBlacklistService : ITokenBlacklistService
    {
        private readonly IMemoryCache _cache;

        public TokenBlacklistService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void RevokeToken(string jti, DateTime expiration)
        {
            _cache.Set(jti, true, expiration);
        }

        public bool IsTokenRevoked(string jti)
        {
            return _cache.TryGetValue(jti, out _);
        }
    }


}
