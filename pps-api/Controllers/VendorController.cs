using Microsoft.AspNetCore.Mvc;

namespace pps_api.Controllers
{
    public class VendorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
