using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Domain.RequestModel;
using TaskManagement.Domain.ResponseModel;

namespace TaskManagement.Repository.Interface
{
    public interface IUserScreen
    {
        Task<ResultModel> RegisterUserScreenAsync(UserScreenMasterRequest request);
        Task<List<UserScreenMasterResponse>> GetAllActiveUserScreensAsync();
        Task<List<UserScreenMasterResponse>> GetAllUserScreensAsync();
        Task<UserScreenMasterResponse> GetUserScreenAsync(int screenId);
        Task<List<UserRoleScreenMappingResponse>> GetUserdetailAsync();
    }
}
