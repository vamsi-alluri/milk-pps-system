using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace pps_api.Entities
{
    /// <summary>
    /// Represents a dairy or processing facility where collected products are processed.
    /// Includes location, capabilities, and cost parameters for operations.
    /// </summary>
    public class ProcessingPlant
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Location { get; set; }
        public string? Address { get; set; }

        [Required]
        [MaxLength(50)]
        public string Capabilities { get; set; } = string.Empty;

        [Precision(18, 2)]
        public decimal? Cost { get; set; }
    }
}
