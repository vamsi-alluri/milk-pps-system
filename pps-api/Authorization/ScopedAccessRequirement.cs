using Microsoft.AspNetCore.Authorization;

namespace pps_api.Authorization
{
    public class ScopedAccessRequirement : IAuthorizationRequirement
    {
        public string RequiredDepartment { get; }
        public int MinimumRoleLevel { get; }

        public ScopedAccessRequirement(string requiredDepartment, int minimumRoleLevel)
        {
            RequiredDepartment = requiredDepartment;
            MinimumRoleLevel = minimumRoleLevel;
        }
    }

}
