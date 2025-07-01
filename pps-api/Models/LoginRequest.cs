using System.Text.Json.Serialization;

namespace pps_api.Models
{
    public class LoginRequest
    {
        [JsonPropertyName("rememberMe")]
        public bool RememberMe { get; set; }

        [JsonIgnore]
        public string? Jwt { get; set; }

        [JsonPropertyName("credentials")]
        public Credentials? Creds { get; set; }
    }
}
