namespace pps_api.Entities
{
    public class Drivers
    {
        public int? DriverID { get; set; }
        public string DriverName { get; set; }
        public string? DriverPhoneNumber { get; set; }
        public string? DriverLicenseNumber { get; set; }
        public DateTime? DriverLicenseValidUpTo { get; set; }
        public string? DriverBadgeNumber { get; set; }

        public Drivers(int? DriverID_, string DriverName_, string? DriverPhoneNumber_, string? DriverLicenseNumber_, DateTime? DriverLicenseValidUpTo_, string? DriverBadgeNumber_)
        {
            this.DriverID = DriverID_;
            this.DriverName = DriverName_;
            this.DriverPhoneNumber = DriverPhoneNumber_;
            this.DriverLicenseNumber = DriverLicenseNumber_;
            this.DriverLicenseValidUpTo = DriverLicenseValidUpTo_;
            this.DriverBadgeNumber = DriverBadgeNumber_;
        }
    }
}
