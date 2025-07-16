using pps_api.Models;

namespace pps_api.Managers
{
    public interface ILoginManager
    {
        // public bool UserRegistration(Credentials creds);
        public bool RegisterNewUser(RegisterNewUser newUser);
        public bool AuthenticateUser(LoginRequest loginRequest, out string? token_str, out DateTime expirationDate);
        public bool DeleteUserSession(LoginRequest loginRequest);
        public string? ListLogins();
    }
}