using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Api.Core.Helper;
using TaskManagement.Domain.Common;
using TaskManagement.Domain.RequestModel;
using TaskManagement.Domain.ResponseModel;
using TaskManagement.Repository.Interface;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRegistration _projectRegistration;

        public ProjectController(IProjectRegistration projectRegistration) // default constructor of class
        {
            _projectRegistration = projectRegistration;
        }

        [HttpGet]
        [Route("getAllProject")]
        public async Task<List<ProjectMasterResponse>> GetAllProjectAsync()
        {
            return await _projectRegistration.GetAllProjectAsync();
        }

        [HttpGet]
        [Route("getProjectWithPagination")]
        public async Task<List<ProjectMasterResponse>> GetProjectAsyncWithPagination([FromQuery] int skipRow, int rowSize, int currentPage, string searchtext, string status)
        {
            //int ParentUserId = CommonHelper.GetCurrentUserId(HttpContext);
            return await _projectRegistration.GetProjectAsyncWithPagination( skipRow, rowSize, currentPage, searchtext, status);
        }

        [HttpGet]
        [Route("getActiveProject")]
        public async Task<List<ProjectMasterResponse>> GetActiveProjectAsync()
        {
            return await _projectRegistration.GetActiveProjectAsync();
        }

        [HttpGet]
        [Route("getManagerDDL")]
        public async Task<List<UserResponse>> GetManagerDDLAsync()
        
        {
            return await _projectRegistration.GetManagerDDLAsync();
        }

        [HttpPost]
        [Route("registerProject")]
        public async Task<IActionResult> RegisterProjectAsync([FromBody] ProjectMasterRequest projectRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid Data Model");
            CommonHelper.SetUserInformation(ref projectRequest, projectRequest.ProjectId, HttpContext);
            var resultModel = await _projectRegistration.RegisterProjectAsync(projectRequest);

            return ReturnResposneType(resultModel);
        }



        private IActionResult ReturnResposneType(ResultModel result)
        {
            if (result.StatusCode == ResultCode.SuccessfullyCreated)
                return Created("", result);
            else if (result.StatusCode == ResultCode.SuccessfullyUpdated)
                return Ok(result);
            else if (result.StatusCode == ResultCode.Unauthorized)
                return Unauthorized(result);
            else if (result.StatusCode == ResultCode.DuplicateRecord)
                return Conflict(result);
            else if (result.StatusCode == ResultCode.RecordNotFound)
                return NotFound(result);
            else if (result.StatusCode == ResultCode.NotAllowed)
                return NotFound(result);
            else if (result.StatusCode == ResultCode.ExceptionThrown)
                return NotFound(result);

            return null;
        }
    }
}
