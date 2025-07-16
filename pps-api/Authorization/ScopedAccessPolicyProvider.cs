using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace pps_api.Authorization
{
    public class ScopedAccessPolicyProvider : IAuthorizationPolicyProvider
    {
        const string POLICY_PREFIX = RequireAccessScopeAttribute.PolicyPrefix;

        // Fallback default policy provider
        private readonly DefaultAuthorizationPolicyProvider fallbackPolicyProvider;

        public ScopedAccessPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            fallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
            fallbackPolicyProvider.GetDefaultPolicyAsync();

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() =>
            fallbackPolicyProvider.GetFallbackPolicyAsync();

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.StartsWith($"{POLICY_PREFIX}:", StringComparison.OrdinalIgnoreCase))
            {
                var parts = policyName.Substring(POLICY_PREFIX.Length + 1).Split(':');
                if (parts.Length == 2
                    && !string.IsNullOrWhiteSpace(parts[0])
                    && int.TryParse(parts[1], out int roleLevel))
                {
                    var department = parts[0];

                    var policy = new AuthorizationPolicyBuilder();
                    policy.AddRequirements(new ScopedAccessRequirement(department, roleLevel));
                    return Task.FromResult<AuthorizationPolicy?>(policy.Build());
                }
            }

            return fallbackPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
