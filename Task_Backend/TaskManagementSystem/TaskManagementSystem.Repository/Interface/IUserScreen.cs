using TaskManagementSystem.Domain.RequestModel;
using TaskManagementSystem.Domain.ResponseModel;

namespace TaskManagementSystem.Repository.Interface
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
