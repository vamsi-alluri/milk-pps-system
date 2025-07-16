using Microsoft.EntityFrameworkCore;
using pps_api.Entities;
using pps_api.Managers.Interfaces;
using pps_api.Models;
using pps_api.Services;
using pps_api.Utils;
using System.IdentityModel.Tokens.Jwt;

namespace pps_api.Managers
{
    public class UserManagementManager : IUserManagementManager
    {
        ITokenService _tokenService;
        AppDbContext _dbContext;
        public UserManagementManager(ITokenService tokenService, AppDbContext dbContext)
        {
            _tokenService = tokenService;
            _dbContext = dbContext;
        }

        public bool AreCredentialsValid(Credentials creds, out UserIdentity? userIdentityData)
        {
            userIdentityData = null;
            if (string.IsNullOrEmpty(creds?.Username) || string.IsNullOrEmpty(creds?.Password))
            {
                throw new ArgumentNullException("Username and Password are required fields.");
            }

            var user_identity_data = _dbContext.Identities.Include(i => i.UserDepartmentMaps).ThenInclude(ud => ud.Department).FirstOrDefault(i => i.Username == creds.Username);
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
            if (loginRequest == null)
            {
                throw new ArgumentNullException("LoginRequest cannot be null.");
            }

            var creds = loginRequest.Creds;

            token_str = null;
            expirationDate = DateTime.UtcNow.AddDays(1);
            if (creds == null)
            {
                throw new ArgumentNullException("Either Credentials or JWT must be provided.");
            }
            else
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

        public bool ChangePassword(ChangePassword changePasswordRequest)
        {
            var identity = _dbContext.Identities.FirstOrDefault(i => i.Username == changePasswordRequest.Username);

            if (identity != null)
            {
                identity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(changePasswordRequest.NewPassword);

                identity.IsPasswordStale = false; // Reset stale status after password change
                identity.LastUpdated = DateTime.UtcNow;
                _dbContext.Identities.Update(identity);
                _dbContext.SaveChanges();
                return true;
            }
            else
            {
                return false; // User not found
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

        private bool IsPasswordValid(string password, out List<string> errors)
        {
            errors = new List<string>();

            if (string.IsNullOrWhiteSpace(password)) errors.Add("Password is required.");
            if (password.Length < 16) errors.Add("Password must be at least 16 characters.");
            if (!password.Any(char.IsUpper)) errors.Add("Password must contain at least one uppercase letter.");
            if (!password.Any(char.IsLower)) errors.Add("Password must contain at least one lowercase letter.");
            if (!password.Any(char.IsDigit)) errors.Add("Password must contain at least one number.");
            if (!password.Any(ch => !char.IsLetterOrDigit(ch))) errors.Add("Password must contain at least one special character.");

            return errors.Count == 0;
        }

        public bool RegisterNewUser(RegisterNewUser newUser)
        {
            if (newUser == null 
                || newUser.Creds == null 
                ||  string.IsNullOrEmpty(newUser.Creds.Username) 
                || string.IsNullOrEmpty(newUser.Creds.Password))
            {
                throw new ArgumentNullException("Credentials cannot be null or empty.");
            }
            // Check if the user already exists
            var existingUser = _dbContext.Identities.FirstOrDefault(i => i.Username == newUser.Creds.Username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            if (IsPasswordValid(newUser.Creds.Password, out List<string> passwordErrors) == false)
            {
                throw new ArgumentException(string.Join(", ", passwordErrors));
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(newUser.Creds.Password);
            var password_exists_for_any_user = _dbContext.Identities.FirstOrDefault(i => i.PasswordHash == passwordHash);
            if (password_exists_for_any_user != null)
            {
                throw new InvalidOperationException("Choose a different password.");
            }

            if (newUser.AccessScopes == null || newUser.AccessScopes.Count == 0)
            {
                throw new ArgumentException("At least one access scope is required.");
            }

            var userDepartmentMappings = new List<UserDepartmentMapping>();

            foreach (var accessScope in newUser.AccessScopes)
            {
                if (string.IsNullOrWhiteSpace(accessScope.DepartmentName) || !int.TryParse(accessScope.RoleLevel, out int roleLevel))
                {
                    throw new ArgumentException("Invalid department name or role level.");
                }

                Department? department = null;

                if (!string.IsNullOrWhiteSpace(accessScope.DepartmentId) &&
                    int.TryParse(accessScope.DepartmentId, out int depId))
                {
                    department = _dbContext.Departments.FirstOrDefault(d => d.ID == depId);
                }

                if (department == null)
                {
                    department = _dbContext.Departments
                        .FirstOrDefault(d => d.Name == accessScope.DepartmentName);
                }

                if (department == null)
                {
                    if (newUser.AddNewDepartment)
                    {
                        department = new Department { Name = accessScope.DepartmentName };
                        _dbContext.Departments.Add(department);
                        _dbContext.SaveChanges(); // So department.ID is available
                    }
                    else
                    {
                        throw new InvalidOperationException($"Department '{accessScope.DepartmentName}' not found.");
                    }
                }

                userDepartmentMappings.Add(new UserDepartmentMapping
                {
                    Department = department,
                    RoleLevel = roleLevel
                });
            }


            // Create a new Identity object and save it to the database
            var newIdentity = new UserIdentity
            {
                Active = newUser.Active ?? false,
                UserDepartmentMaps = userDepartmentMappings,
                Username = newUser.Creds.Username,
                LastUpdated = DateTime.UtcNow,
                Created = DateTime.UtcNow,
                PasswordHash = passwordHash,
                IsPasswordStale = newUser.IsPasswordStale ?? true
            };
            _dbContext.Identities.Add(newIdentity);
            _dbContext.SaveChanges();

            return true;
        }

        public bool DeleteUserSession(LoginRequest loginRequest)
        {
            if (loginRequest == null)
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
