using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.RequestModel;
using TaskManagementSystem.Domain.ResponseModel;

namespace TaskManagementSystem.Repository.Interface
{
    public interface IUserAccessManager
    {
        Task<ResultModel> RegisterUserAccessAsync(List<UserAccessManagerRequest> requestModel);
        Task<List<UserAccessManagerResponse>> GetAllActiveUserAccessAsync();
        Task<List<UserAccessManagerResponse>> GetAllUserAccessAsync();
        Task<UserAccessManagerResponse> GetUserAccessAsync(int accessManagerId);
        Task<List<UserAccessManagerResponse>> GetUserAccessBasedOnRoleIdAsync(int roleId);
        Task<List<UserResponse>> GetAllUserbyDepartmentAsync(int department);
    }
}
