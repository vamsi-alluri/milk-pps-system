using pps_api.Entities;
using pps_api.Models;

namespace pps_api.Managers.Interfaces
{
    public interface IVendorManager
    {
        public List<Vendor>? GetAllVendors();
        public Vendor? GetVendorById(int VendorId);
    }
}