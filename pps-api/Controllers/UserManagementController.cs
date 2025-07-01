using Microsoft.AspNetCore.Mvc;

namespace pps_api.Controllers
{
    public class UserManagementController : Controller
    {
        public UserManagementController() 
        {

            // Notes for user deletion:
            //  Can't self delete.
            // To delete a user, the supervisor will use this endpoint.
            // The supervisor will have an UI to select the user, and this endpoint will be called with the current user(Supervisor) credentials, with a specific user id.
        }

    }
}
