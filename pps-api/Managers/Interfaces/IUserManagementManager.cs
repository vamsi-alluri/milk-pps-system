using pps_api.Entities;
using pps_api.Models;

namespace pps_api.Managers.Interfaces
{
    public interface IUserManagementManager
    {
        // public bool UserRegistration(Credentials creds);
        public bool RegisterNewUser(RegisterNewUser newUser);
        public bool AuthenticateUser(LoginRequest loginRequest, out string? token_str, out DateTime expirationDate);
        public bool DeleteUserSession(LoginRequest loginRequest);
        public bool ChangePassword(ChangePassword changePasswordRequest);
        public bool AreCredentialsValid(Credentials creds, out UserIdentity? userIdentityData);
        public string? ListLogins();
    }
}