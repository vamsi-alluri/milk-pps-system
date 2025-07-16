using System.Text.Json.Serialization;

namespace pps_api.Models
{
    public class LoginRequest
    {
        [JsonPropertyName("rememberMe")]
        public bool RememberMe { get; set; }

        [JsonPropertyName("credentials")]
        public Credentials? Creds { get; set; }
    }
}
