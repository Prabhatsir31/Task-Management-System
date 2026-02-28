using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Repository.Context;
using TaskManagement.Repository.Entities;
using TaskManagement.Repository.Interface;
using TaskManagement.Domain.ResponseModel;
using TaskManagement.Domain.Common;

namespace TaskManagement.Repository.Repositories
{
    public class TaskHistoryRepository : ITaskHistory
    {
        private readonly ITaskContextFactory _contextFactory;
        private readonly IMapper _mapper;
        private readonly ITaskHistory _taskHistory;
        public TaskHistoryRepository(ITaskContextFactory contextFactory,
            IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<ResultModel> AddTaskHistory(TaskTransaction task)
        {
            var resMessage = "Task HIstory ";
            try
            {
                using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                TaskHistory history = new TaskHistory();
                history.TaskId = task.TaskId;
                history.TaskType = task.TaskTypeMaster?.TaskName;
                history.Description = task.Description;
                history.Priority = task.Priority;
                history.AssignedTo = task.UserManager?.UserName;
                history.ExceptedStartDate = task.ExceptedStartDate;
                history.ExceptedEndDate = task.ExceptedEndDate;
                history.Status = task.Status;
                history.IsActive = task.IsActive;

                kUrgeTruckContext.Add(history);
                await kUrgeTruckContext.SaveChangesAsync();

                return ResultModelFactory.UpdateSucess(resMessage);
                

            }
            catch (Exception ex)
            {
                Logger.Error("Error while register Task History " + ex);
                return ResultModelFactory.CreateFailure(ResultCode.ExceptionThrown, TaskMessages.Error_while_addupdate_Task, ex);
            }
        }

        public async Task<List<TaskHistory>> GetTaskHistory(int taskId)
        {
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var taskHistory = await kUrgeTruckContext.TaskHistory.Where(x => x.TaskId == taskId).OrderByDescending(x=>x.TaskId).ToListAsync();
            return taskHistory;
        }
    }
}
