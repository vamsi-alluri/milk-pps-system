using System.ComponentModel.DataAnnotations;

namespace pps_api.Entities
{
    /// <summary>
    /// Represents a vendor who supplies one or more sub-products to the company.
    /// Contains business details, tax identifiers, and banking information.
    /// A vendor may be associated with one or more routes over time.
    /// </summary>
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
        public string? Location { get; set; }
        public string? Address { get; set; }
        public int? VendorTypeId { get; set; } // Foreign key to VendorType
        public VendorType? Type { get; set; } // Type of vendor, e.g., Manufacturer, Trader, Producer, etc.
        public string? GstNumber { get; set; }
        public string? FssaiNumber { get; set; }
        public string? EncryptedPAN { get; set; }

        // ToDo: Credit period in days. - Once it is expired, a voucher should be created for the vendor - mail notification.
        public uint? CreditPeriod { get; set; } // Credit period in days, e.g., 30 days, 60 days, etc.
        public int? VendorBankDetailsId { get; set; }
        public VendorBankDetails? BankDetails { get; set; }
        public ICollection<VendorRoute> VendorRoutes { get; set; } = new List<VendorRoute>();
        public ICollection<VendorSubProduct> VendorSubProducts { get; set; } = new List<VendorSubProduct>();
    }
}
