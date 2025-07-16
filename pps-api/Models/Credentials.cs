using System.Text.Json.Serialization;

namespace pps_api.Models
{
    public class Credentials
    {
        [JsonPropertyName("username")]
        public string? Username { get; set; }
        
        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}
