using System.ComponentModel.DataAnnotations;

namespace pps_api.Entities
{
    public class VendorBankDetails
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AccountNumber { get; set; } = string.Empty;
        [Required]
        public string IFSCCode { get; set; } = string.Empty;
        [Required]
        public string BankName { get; set; } = string.Empty;
        [Required]
        public string BranchName { get; set; } = string.Empty;
        [Required]
        public string AccountHolderName { get; set; } = string.Empty;
    }

}
