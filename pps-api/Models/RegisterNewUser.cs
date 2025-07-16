using pps_api.Entities;
using System.Text.Json.Serialization;

namespace pps_api.Models
{
    public class RegisterNewUser
    {
        [JsonPropertyName("credentials")]
        public Credentials? Creds { get; set; }

        [JsonPropertyName("access-scopes")]
        public List<RegisterNewUserDepartmentRoles>? AccessScopes { get; set; }

        [JsonPropertyName("active")]
        public bool? Active { get; set; } = false;

        [JsonPropertyName("is-password-stale")]
        public bool? IsPasswordStale { get; set; } = true;

        [JsonPropertyName("add-new-department")]
        public bool AddNewDepartment { get; set; } = false;

    }

    [Serializable]
    public class RegisterNewUserDepartmentRoles
    {
        [JsonPropertyName("department-id")]
        public string? DepartmentId { get; set; }

        [JsonPropertyName("department-name")]
        public string? DepartmentName { get; set; }

        [JsonPropertyName("role-level")]
        public string? RoleLevel { get; set; }
    }
}
