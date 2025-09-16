namespace pps_api.Entities
{
    /// <summary>
    /// Junction table that maps a vendor to a route over a defined time period.
    /// Supports temporal tracking of vendor-route associations with stop order and validity range.
    /// </summary>
    public class VendorRoute
    {
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; } = null!;

        public int RouteId { get; set; }
        public Route Route { get; set; } = null!;

        public int StopOrder { get; set; }

        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime? EndDate { get; set; } // Null = currently active
    }
}
