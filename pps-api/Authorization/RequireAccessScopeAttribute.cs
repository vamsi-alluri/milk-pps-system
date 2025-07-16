using Microsoft.AspNetCore.Authorization;

namespace pps_api.Authorization
{
    public class RequireAccessScopeAttribute : AuthorizeAttribute
    {
        public RequireAccessScopeAttribute(string department, int minRoleLevel)
        {
            Policy = $"{PolicyPrefix}:{department}:{minRoleLevel}";
        }

        public const string PolicyPrefix = "AccessScope";
    }
}
