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

        public List<Vendor>? GetAllVendors()
        {
            var allVendors = _dbContext.Vendors // Invalid object name vendors.
                .Include(v => v.Type)
                .Include(v => v.BankDetails)
                .Include(v => v.VendorRoutes)
                    .ThenInclude(vr => vr.Route)
                .Include(v => v.VendorSubProducts)
                    .ThenInclude(vsp => vsp.SubProduct)
                .ToList();
            return allVendors;
        }

        public Vendor? GetVendorById(int VendorId)
        {
            var vendor = _dbContext.Vendors
                .Include(v => v.Type)
                .Include(v => v.BankDetails)
                .Include(v => v.VendorRoutes)
                    .ThenInclude(vr => vr.Route)
                .Include(v => v.VendorSubProducts)
                    .ThenInclude(vsp => vsp.SubProduct)
                .FirstOrDefault(v => v.Id == VendorId);

            return vendor;
        }
    }
}
