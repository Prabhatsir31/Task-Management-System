using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Repository.Entities;
using TaskManagement.Domain.ResponseModel;

namespace TaskManagement.Repository.Interface
{
    public interface ITaskHistory
    {
        Task<ResultModel> AddTaskHistory(TaskTransaction task);
        Task<List<TaskHistory>> GetTaskHistory(int taskId);
    }
}
