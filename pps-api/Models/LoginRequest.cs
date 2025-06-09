using pps_models_lib;
using System.Text.Json.Serialization;

namespace pps_api.Models
{
    public class LoginRequest
    {
        [JsonPropertyName("rememberMe")]
        public bool RememberMe { get; set; }

        [JsonPropertyName("jwt")]
        public string? Jwt { get; set; }

        [JsonPropertyName("credentials")]
        public Credentials creds { get; set; }
    }
}
