using System.ComponentModel.DataAnnotations;

namespace pps_api.Entities
{
    /// <summary>
    /// Represents a physical path taken by vehicles for vendor visits,
    /// typically originating from a processing plant.
    /// Each route can have multiple vendors in a defined stop order.
    /// </summary>
    public class Route
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RouteCode { get; set; } = string.Empty;

        [Required]
        public string RouteName { get; set; } = string.Empty;

        public int ProcessingPlantId { get; set; }
        public ProcessingPlant AssociatedProcessingPlant { get; set; } = null!;

        public ICollection<VendorRoute> VendorRoutes { get; set; } = new List<VendorRoute>();
    }

}
