using Microsoft.AspNetCore.Authorization;
using pps_api.Enums;

namespace pps_api.Authorization
{
    public class RequireAccessScopeAttribute : AuthorizeAttribute
    {
        public RequireAccessScopeAttribute(string department, int minRoleLevel)
        {
            Policy = $"{PolicyPrefix}:{department}:{minRoleLevel}";
        }
        public RequireAccessScopeAttribute(string department, Roles minRoleLevel)
        {
            Policy = $"{PolicyPrefix}:{department}:{(int)minRoleLevel}";
        }

        public const string PolicyPrefix = "AccessScope";
    }
}
