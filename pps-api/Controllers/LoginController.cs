using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using pps_api.Managers;
using pps_api.Models;
using pps_models_lib;
using pps_api.Services;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pps_api.Controllers
{
    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("usepaa")]
        public string Get(string username, string passwordMask)
        {
            Console.WriteLine("Login: Get");
            // Until the project is connected to a database, we will use hardcoded credentials for demonstration purposes.
            if (username == "admin" && passwordMask == "admin1234")
            {
                return "Login successful!";
            }
            else
            {
                Credentials creds = new Credentials
                {
                    username = username,
                    password = passwordMask

                };
                var json_creds = JsonSerializer.Serialize<Credentials>(creds);

                return json_creds;
            }
        }

        // POST api/login
        [HttpPost("usepaa")]
        public IActionResult Post([FromBody] LoginRequest login_request)
        {
            Console.WriteLine("Login: Post");

            //Credentials creds = System.Text.Json.JsonSerializer.Deserialize<Credentials>(value);
            
            if (LoginManager.AuthenticateUser(login_request.creds))
            {
                var access_or_refresh_token = LoginManager.HandleTokenization(login_request);

                Response.Cookies.Append("refreshToken", access_or_refresh_token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTime.UtcNow.AddDays(login_request.RememberMe ? 30 : 1)
                });

                return Ok(new { LoginManager.HandleTokenization() });
            }


            return Unauthorized();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
