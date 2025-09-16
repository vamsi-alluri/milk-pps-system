using System.ComponentModel.DataAnnotations;

namespace pps_api.Entities
{
    /// <summary>
    /// Classifies vendors based on their role in the supply chain,
    /// such as Producer, Trader, or Manufacturer.
    /// <br></br>Producer: A vendor who produces goods, such as a farmer.
    /// <br></br>Manufacturer: A vendor who manufactures goods, such as a dairy plant.
    /// <br></br>Trader: A vendor who trades goods, such as a distributor.
    /// </summary>
    public class VendorType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty; // e.g., Manufacturer, Trader
    }

}
