using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pps_api.Managers;
using pps_api.Models;

namespace pps_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {

        private readonly ILoginManager Manager;

        public RegisterController(ILoginManager manager)
        {
            Manager = manager;
        }

        // POST api/register
        [HttpPost]
        public IActionResult Post([FromBody] LoginRequest login_request)
        {
            try
            {
                if (Manager.SetUserPassword(login_request.Creds))
                {
                    return Ok(true);
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                // TODO: This shouldn't always return BadRequest, consider using a more specific error handling strategy.
                return BadRequest(new { message = ex.Message });
            }
            return Ok(false);
        }
    }
}
