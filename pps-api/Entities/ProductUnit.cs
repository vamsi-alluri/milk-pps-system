using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace pps_api.Entities
{
    /// <summary>
    /// Defines a measurable unit (e.g., kg, litre) and conversion to a base unit.
    /// Used for pricing, logistics, and reporting consistency.
    /// </summary>
    public class ProductUnit
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty; // e.g., "kg", "litre"

        public string? Description { get; set; }

        [Required]
        [Precision(18, 4)]
        public decimal ConversionFactor { get; set; } = 1.0m; // To base unit (e.g., 1 kg = 1000 g)
    }

}
