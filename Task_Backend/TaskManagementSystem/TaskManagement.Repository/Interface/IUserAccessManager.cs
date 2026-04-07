using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Domain.RequestModel;
using TaskManagement.Domain.ResponseModel;

namespace TaskManagement.Repository.Interface
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
