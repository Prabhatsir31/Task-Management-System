using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Repository.Entities;
using TaskManagementSystem.Domain.ResponseModel;

namespace TaskManagementSystem.Repository.Interface
{
    public interface ITaskHistory
    {
        Task<ResultModel> AddTaskHistory(TaskTransaction task);
        Task<List<TaskHistory>> GetTaskHistory(int taskId);
    }
}
