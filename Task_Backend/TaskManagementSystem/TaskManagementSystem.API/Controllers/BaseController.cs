using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Repository.Entities;

namespace TaskManagementSystem.API.Controllers
{
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        string str = "";
        // returns the current authenticated account (null if not logged in)
        public UserManager Account => (UserManager)HttpContext.Items["Account"];
    }
}
