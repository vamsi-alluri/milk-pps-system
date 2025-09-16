using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace pps_api.Entities
{
    /// <summary>
    /// Defines a general product (e.g., "Toned Milk") with a default unit.
    /// Categorized under a product type like Milk or Fuel.
    /// </summary>
    public class SubProduct
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; } = null!;

        public int? ProductUnitId { get; set; } // Default unit
        public ProductUnit? ProductUnit { get; set; }

        public string? HSNCode { get; set; }
    }
}
