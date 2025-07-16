using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using pps_api.Entities;
using pps_api.Managers.Interfaces;
using pps_api.Models;
using pps_api.Services;
using pps_api.Utils;

namespace pps_api.Managers
{
    public class VendorManager : IVendorManager
    {
        AppDbContext _dbContext;
        public VendorManager(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Vendor>? GetVendors()
        {
            return new List<Vendor>();
        }

        public Vendor? GetVendors(int VendorId)
        {
            var vendor = _dbContext.Vendors
                .Include(v => v.SubProducts)
                .ThenInclude(sp => sp.ProductType)
                .FirstOrDefault(v => v.Id == VendorId);

            return vendor;
        }
    }
}
