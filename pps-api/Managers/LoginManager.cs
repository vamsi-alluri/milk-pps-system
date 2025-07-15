using pps_api.Entities;
using pps_api.Models;
using pps_api.Services;
using pps_api.Utils;
using System.IdentityModel.Tokens.Jwt;

namespace pps_api.Managers
{
    public class LoginManager : ILoginManager
    {
        ITokenService _tokenService;
        AppDbContext _dbContext;
        public LoginManager(ITokenService tokenService, AppDbContext dbContext)
        {
            _tokenService = tokenService;
            _dbContext = dbContext;
        }

        private bool AreCredentialsValid(Credentials creds, out UserIdentity? userIdentityData)
        {
            userIdentityData = null;
            if (string.IsNullOrEmpty(creds?.Username) || string.IsNullOrEmpty(creds?.Password))
            {
                throw new ArgumentNullException("Username and Password are required fields.");
            }

            var user_identity_data = _dbContext.Identities.Where(i => i.Username == creds.Username).FirstOrDefault();
            if (user_identity_data == null)
            {
                // User does not exist
                return false;
            }

            if (!user_identity_data.Active)
            {
                // User is not active
                // Log the inactive user attempt.
                return false;
            }

            if (BCrypt.Net.BCrypt.Verify(creds.Password, user_identity_data.PasswordHash))
            {
                userIdentityData = user_identity_data;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool AuthenticateUser(LoginRequest loginRequest, out string? token_str, out DateTime expirationDate)
        {
            var creds = loginRequest.Creds;
            var jwt_str = loginRequest.Jwt;

            // Preference creds over jwt.
            token_str = null;
            expirationDate = DateTime.UtcNow.AddDays(1);
            if (creds == null)
            {
                if (jwt_str == null)
                {
                    // It should never happen, but just in case.
                    throw new ArgumentNullException("Either Credentials or JWT must be provided.");
                }
                else
                {
                    if (_tokenService.ValidateToken(jwt_str, out JwtSecurityToken? decodedJwtToken))
                    {
                        if (_tokenService.VerifyToken(decodedJwtToken) && _tokenService.VerifyStoredToken(loginRequest?.Creds?.Username, token_str))
                        {
                            return true; // Token is valid.
                        }
                        else
                        {
                            return false; // Token is valid and verification failed.
                        }
                    }
                    else
                    {
                        // TODO: Received token is not of JWT type.
                        return false;
                    }
                }
            }
            else // Disregard jwt_str if creds are provided and issue a new one.
            {
                if (AreCredentialsValid(creds, out UserIdentity? userIdentityData))
                {
                    expirationDate = loginRequest.RememberMe
                        ? DateTime.UtcNow.AddDays(7) // Long expiration for remember me
                        : DateTime.UtcNow.AddDays(1); // Short expiration for normal login

                    var userAuthInfo = new UserAuthInfo
                    {
                        UserId = creds.Username,
                        AccessScopes = userIdentityData.ToAccessScopes(),
                        ExpirationDate = expirationDate
                    };

                    token_str = _tokenService.GenerateJwtToken(userAuthInfo);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // TODO: Need to re-implement this method with full user with department and role functionality.
        //public bool UserRegistration(Credentials creds)
        //{
        //    if (creds == null || string.IsNullOrEmpty(creds.Username) || string.IsNullOrEmpty(creds.Password))
        //    {
        //        throw new ArgumentNullException("Credentials cannot be null or empty.");
        //    }
        //    // Check if the user already exists
        //    var existingUser = _dbContext.Identity.FirstOrDefault(i => i.Username == creds.Username);
        //    if (existingUser != null)
        //    {
        //        throw new InvalidOperationException("User already exists.");
        //    }

        //    // Create a new Identity object and save it to the database
        //    var newIdentity = new Identity(creds)
        //    {
        //        Active = true, // Set default active status
        //        Role = "User", // Set default role
        //        LastUpdated = DateTime.UtcNow,
        //        Created = DateTime.UtcNow,
        //        PasswordHash = BCrypt.Net.BCrypt.HashPassword(creds.Password), // Hash the password
        //        IsPasswordStale = false // Set default password stale status. TODO: If an admin creates a user, set this to true.
        //    };

        //    _dbContext.Identity.Add(newIdentity);
        //    _dbContext.SaveChanges();

        //    return true;
        //}

        public bool SetUserPassword(Credentials creds)
        {
            if (creds == null || string.IsNullOrEmpty(creds.Username) || string.IsNullOrEmpty(creds.Password))
            {
                throw new ArgumentNullException("Credentials cannot be null or empty.");
            }
            // Check if the user already exists
            var existingUser = _dbContext.Identities.FirstOrDefault(i => i.Username == creds.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            // Create a new Identity object and save it to the database
            var newIdentity = new UserIdentity
            {
                Active = true, // Set default active status
                UserDepartments = new List<UserDepartment> { new UserDepartment 
                    { 
                        Department = new Department 
                        { 
                            Name = "ANY" // Set default department
                        },
                        RoleLevel = 1 // Set default role level
                    }
                },

                LastUpdated = DateTime.UtcNow,
                Created = DateTime.UtcNow,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(creds.Password), // Hash the password
                IsPasswordStale = false // Set default password stale status. TODO: If an admin creates a user, set this to true.
            };

            _dbContext.Identities.Add(newIdentity);
            _dbContext.SaveChanges();

            return true;
        }

        public bool DeleteUserSession(LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Jwt))
            {
                throw new ArgumentNullException("JWT is required for user session deletion.");
            }
            if (!AuthenticateUser(loginRequest, out string? token_str, out DateTime expirationDate))
            {
                throw new UnauthorizedAccessException("Invalid credentials or JWT.");
            }
            if (loginRequest.Creds == null || string.IsNullOrEmpty(loginRequest.Creds.Username))
            {
                throw new ArgumentNullException("Username cannont be null.");
            }

            _tokenService.RemoveRefreshToken(loginRequest.Creds.Username);
            return true;
        }

        public string? ListLogins()
        {
            // This method is for debugging purposes only.
            // It lists all active logins stored in the memory as key value pair in each line.

            return _tokenService.GetAllRefreshTokens();
        }
    }
}
