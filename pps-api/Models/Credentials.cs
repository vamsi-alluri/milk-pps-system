using System.Text.Json.Serialization;

namespace pps_api.Models
{
    public class Credentials
    {
        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }
        
        [JsonPropertyName("password")]
        public string? Password { get; set; }
        
        [JsonIgnore]
        public string? PasswordHash { get; set; }
    }
}
