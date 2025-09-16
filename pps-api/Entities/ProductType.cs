using System.ComponentModel.DataAnnotations;

namespace pps_api.Entities
{
    /// <summary>
    /// Represents the category of a sub-product, such as Milk, Fuel, or Packaging.
    /// Used for classification, reporting, and applying category-level rules.
    /// </summary>
    public class ProductType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty; // e.g., "Milk", "Fuel", "Chemical"
    }

}
