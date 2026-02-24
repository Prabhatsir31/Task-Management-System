using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManagement.Domain.ResponseModel;
using TaskManagement.Repository.Interface;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationConfigurationController : ControllerBase
    {
        private readonly IApplicationConfiguration _applicationConfiguration;

        public ApplicationConfigurationController(IApplicationConfiguration applicationConfiguration)
        {
            _applicationConfiguration = applicationConfiguration;
        }

        [HttpGet]
        [Route("GetAllApplicationConfiguration")]
        public async Task<List<ApplicationConfigurationResponse>> GetAllCallInMileStoneAsync()
        {
            var data = await _applicationConfiguration.GetAllApplicationConfigs();
            return data;
        }

        [HttpGet]
        [Route("GetUserNavDesignConfig")]
        public  string GetUserNavDesignConfig()
        {
            var data =  _applicationConfiguration.UserNavDesignConfig();
            return data;
        }

    }
}
