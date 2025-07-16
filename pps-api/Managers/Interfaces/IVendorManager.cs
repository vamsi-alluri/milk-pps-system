using pps_api.Entities;
using pps_api.Models;

namespace pps_api.Managers.Interfaces
{
    public interface IVendorManager
    {
        public List<Vendor>? GetVendors();
        public Vendor? GetVendors(int VendorId);
    }
}