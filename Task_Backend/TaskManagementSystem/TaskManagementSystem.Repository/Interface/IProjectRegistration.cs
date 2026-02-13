using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.RequestModel;
using TaskManagementSystem.Domain.ResponseModel;

namespace TaskManagementSystem.Repository.Interface
{
    public interface IProjectRegistration
    {
        Task<List<ProjectMasterResponse>> GetAllProjectAsync();
        Task<List<ProjectMasterResponse>> GetProjectAsyncWithPagination(int skipRow, int rowSize, int currentPage, string searchtext, string status);
        Task<List<ProjectMasterResponse>> GetActiveProjectAsync();
        Task<List<UserResponse>> GetManagerDDLAsync();
        Task<ResultModel> RegisterProjectAsync(ProjectMasterRequest projectRequest);
    }
}
