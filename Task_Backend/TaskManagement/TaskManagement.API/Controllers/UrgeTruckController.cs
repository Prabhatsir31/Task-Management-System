using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using TaskManagement.Domain.Common;

namespace TaskManagement.Api.Controllers
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
