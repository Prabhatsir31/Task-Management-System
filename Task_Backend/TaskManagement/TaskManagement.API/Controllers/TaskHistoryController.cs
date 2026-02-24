using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Repository.Entities;
using TaskManagement.Repository.Interface;

namespace TaskManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskHistoryController : ControllerBase
    {
        private readonly ITaskHistory _taskHistory;

        public TaskHistoryController(ITaskHistory taskHistory)
        {
            _taskHistory = taskHistory;
        }

        [HttpGet]
        [Route("GetTaskHistory")]
        public async Task<List<TaskHistory>> GetTaskHistory([FromQuery] int taskId)
        {
            return await _taskHistory.GetTaskHistory(taskId);
        }
    }
}
