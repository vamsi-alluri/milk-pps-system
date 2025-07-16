using Microsoft.EntityFrameworkCore;

namespace pps_api.Entities
{
    public class Vendor
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }
        public string? Address { get; set; }
        public bool OnRoute { get; set; } = false;

        public ICollection<SubProduct> SubProducts { get; set; } = new List<SubProduct>();
    }

    public class SubProduct
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public string Name { get; set; } = string.Empty;
        [Precision(18, 2)]
        public decimal? Cost { get; set; }
        public int ProductTypeId { get; set; }
        public Vendor? Vendor { get; set; }
        public ProductType ProductType { get; set; } = null!;
    }

    public class ProductType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // e.g., "Milk", "Fuel"
    }
}
