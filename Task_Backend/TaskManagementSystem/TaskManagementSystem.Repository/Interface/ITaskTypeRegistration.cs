using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.RequestModel;
using TaskManagementSystem.Domain.ResponseModel;

namespace TaskManagementSystem.Repository.Interface
{
    public interface ITaskTypeRegistration
    {
        Task<List<TaskTypeMasterResponse>> GetAllTaskTypeAsync();
        Task<List<TaskTypeMasterResponse>> GetAllActiveTaskTypeAsync();
        Task<List<TaskTypeMasterResponse>> GetTaskTypeAsyncWithPagination(int rowSize, int skipRow, int currentPage, string searchText, int departmentId);
        Task<ResultModel> RegisterTaskTypeAsync(TaskTypeMasterRequest taskTypeRequest);
        Task<List<TaskTypeMasterResponse>> GetAllTaskTypeDepartmentWiseAsync(int departmentId);
    }
}
