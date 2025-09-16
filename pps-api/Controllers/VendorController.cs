using Microsoft.AspNetCore.Mvc;
using pps_api.Managers.Interfaces;

namespace pps_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VendorController : Controller
    {
        private readonly IVendorManager VendorManager;
        public VendorController(IVendorManager vendorManager)
        {
            VendorManager = vendorManager;
        }

        [HttpGet("all")]
        public IActionResult GetVendors()
        {
            // 
            var allVendors = VendorManager.GetAllVendors();
            return Ok(allVendors);
        }

        [HttpGet("{id}")]
        public IActionResult GetVendorById(int vendorId)
        {
            var vendor = VendorManager.GetVendorById(vendorId);
            return Ok(vendor);
        }
    }
}
