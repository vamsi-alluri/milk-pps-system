using System.Text.Json.Serialization;

namespace pps_models_lib
{
    public class Credentials
    {
        [JsonPropertyName("email")]
        public string email { get; set; }
        [JsonPropertyName("username")]
        public string username { get; set; }
        [JsonPropertyName("passwordMask")]
        public string passwordMask { get; set; }

        [JsonPropertyName("password")]
        public string password { get; set; }
    }
}
