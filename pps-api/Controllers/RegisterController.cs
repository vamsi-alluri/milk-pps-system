using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pps_api.Entities;
using pps_api.Managers;
using pps_api.Models;
using pps_api.Authorization;

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
        [RequireAccessScope("ANY", 1)]
        public IActionResult Post([FromBody] RegisterNewUser newUser)
        {
            try
            {
                if (Manager.RegisterNewUser(newUser))
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
