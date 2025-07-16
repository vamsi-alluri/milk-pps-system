using System.Text.Json.Serialization;

namespace pps_api.Models
{
    public class ChangePassword
    {
        [JsonIgnore]
        public string? Username { get; set; }

        [JsonPropertyName("oldPassword")]
        public string? OldPassword { get; set; }

        [JsonPropertyName("newPassword")]
        public string? NewPassword { get; set; }
    }
}
