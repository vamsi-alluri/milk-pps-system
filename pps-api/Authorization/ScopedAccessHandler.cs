using Microsoft.AspNetCore.Authorization;
using pps_api.Models;
using System.Text.Json;

namespace pps_api.Authorization
{
    public class ScopedAccessHandler : AuthorizationHandler<ScopedAccessRequirement>
    {

        const string AnyDepartment = "ANY";
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ScopedAccessRequirement requirement)
        {
            var claimValue = context.User.FindFirst("AccessScopes")?.Value;

            if (string.IsNullOrEmpty(claimValue))
            {
                Console.WriteLine("AccessScopes claim missing.");
                return Task.CompletedTask;
            }

            try
            {
                var scopes = JsonSerializer.Deserialize<List<AccessScope>>(claimValue);

                foreach (var scope in scopes ?? Enumerable.Empty<AccessScope>())
                {
                    Console.WriteLine($"Department: {scope.DepartmentName}, RoleLevel: {scope.RoleLevel}");
                }

                if (scopes != null && scopes.Any(s =>
                    (string.Equals(s.DepartmentName, AnyDepartment, StringComparison.OrdinalIgnoreCase) 
                        || string.Equals(s.DepartmentName, requirement.RequiredDepartment, StringComparison.OrdinalIgnoreCase))
                    && (s.RoleLevel > 0 
                        && s.RoleLevel < 6 
                        && s.RoleLevel >= requirement.MinimumRoleLevel)))
                {
                    context.Succeed(requirement);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Deserialization error: {ex.Message}");
                throw new JsonException("Failed to deserialize AccessScopes claim.", ex);
            }

            return Task.CompletedTask;
        }
    }

}
