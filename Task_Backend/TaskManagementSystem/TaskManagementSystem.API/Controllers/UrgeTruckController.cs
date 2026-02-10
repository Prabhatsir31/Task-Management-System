using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrgeTruckController : ControllerBase
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public UrgeTruckController(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Logger.Debug("Welcome to Kemar Task Management System -API is running");
            return Ok("Welcome to Kemar Task Management System -API is running");
        }

        [HttpGet]
        [Route("me")]
        public IActionResult me()
        {
            Logger.Debug("Welcome to Kemar Task Management System -API is running");
            return Ok("Welcome to Kemar Task Management System -API is running");
        }

        

    }
}
