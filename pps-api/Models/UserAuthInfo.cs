namespace pps_api.Models
{
    public class UserAuthInfo
    {

        public string UserId { get; set; } = string.Empty;
        public int RoleLevel { get; set; } = 0;
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow.AddHours(1);
        public List<AccessScope> AccessScopes { get; set; } = new List<AccessScope>();
    }
}
