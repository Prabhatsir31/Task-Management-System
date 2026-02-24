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
    public class ProjectVersionController : BaseController
    {
        private readonly IProjectVersioning _ProjectVersion;

        public ProjectVersionController(IProjectVersioning projectVersion)
        {
            _ProjectVersion = projectVersion;
        }

        [HttpGet]
        [Route("GetProjectCurrentVersion")]
        public Task<AboutUrgeTruckResponce> GetProjectCurrentVersion()
        {
            return  _ProjectVersion.GetProjectVersion();
        }
    } 
}
