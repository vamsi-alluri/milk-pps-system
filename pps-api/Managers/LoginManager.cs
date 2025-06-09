using pps_api.Models;
using pps_api.Services;
using pps_models_lib;

namespace pps_api.Managers
{
    public class LoginManager
    {
        ITokenService _tokenService;
        public LoginManager()
        {
            _tokenService = new TokenService();
        }

        public static bool AuthenticateUser(Credentials creds)
        {
            if (creds == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(creds.username) || string.IsNullOrEmpty(creds.passwordMask))
            {
                return false;
            }

            if (creds.username == "admin" && creds.passwordMask == "admin1234")
            {
                return true;
            }

            return false;
        }

        public static string HandleTokenization(LoginRequest login_request)
        {

            var accessToken = GenerateJwtToken(expiresMinutes: 15);
            var refreshToken = GenerateRefreshToken(expiresDays: login_request.RememberMe ? 30 : 1);
        }
    }
}
