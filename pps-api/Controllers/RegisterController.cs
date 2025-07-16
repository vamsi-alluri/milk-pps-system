using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using pps_api.Entities;
using pps_api.Models;
using pps_api.Authorization;
using pps_api.Managers.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace pps_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {

        private readonly IUserManagementManager Manager;

        public RegisterController(IUserManagementManager manager)
        {
            Manager = manager;
        }

        // POST api/register
        [HttpPost]
        [RequireAccessScope("ANY", 5)]
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

        // POST api/register/passwordchange
        [HttpPost]
        [Authorize]
        [Route("change-password")]
        public IActionResult Post([FromBody] ChangePassword changePasswordRequest)
        {
            try
            {
                var username = User.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name)?.Value ?? "";
                var creds = new Credentials
                {
                    Username = username,
                    Password = changePasswordRequest.OldPassword
                };

                if (Manager.AreCredentialsValid(creds, out _))
                {
                    changePasswordRequest.Username = username;
                    if (Manager.ChangePassword(changePasswordRequest))
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return Ok("Password updation failed due to db error.");
                    }
                }
                else
                {
                    return Unauthorized("User authentication failed using the provided credentials.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as needed
                // TODO: This shouldn't always return BadRequest, consider using a more specific error handling strategy.
                return Ok(ex.Message);
            }
        }
    }
}
