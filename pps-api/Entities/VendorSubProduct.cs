using Microsoft.EntityFrameworkCore;

namespace pps_api.Entities
{
    /// <summary>
    /// Represents a vendor's offering of a specific sub-product,
    /// priced per unit (e.g., ₹42 per litre), with tax info.
    /// </summary>
    public class VendorSubProduct
    {
        public int VendorId { get; set; }
        public Vendor Vendor { get; set; } = null!;

        public int SubProductId { get; set; }
        public SubProduct SubProduct { get; set; } = null!;

        [Precision(18, 2)]
        public decimal? Cost { get; set; } // Cost per unit

        public int? ProductUnitId { get; set; } // Can override SubProduct default
        public ProductUnit? ProductUnit { get; set; }

        [Precision(4, 4)]
        public decimal? GSTPercentage { get; set; } = null; // Available options: 
                                                            // 0.00, 0.25, 3.00, 5.00, 12.00, 18.00, 28.00
    }
}
