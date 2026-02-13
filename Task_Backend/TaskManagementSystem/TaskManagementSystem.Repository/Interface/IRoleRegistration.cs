using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.RequestModel;
using TaskManagementSystem.Domain.ResponseModel;

namespace TaskManagementSystem.Repository.Interface
{
    public interface IRoleRegistration
    {
        Task<ResultModel> RegisterRoleAsync(RoleMasterRequest request);
        Task<List<RoleMasterResponse>> GetAllActiveRolesAsync();
        Task<List<RoleMasterResponse>> GetAllRolesAsync();
        Task<RoleMasterResponse> GetRoleAsync(int roleId, string roleName = null);
        Task<List<RoleMasterResponse>> GetRoleAsyncWithPagination( int rowSize, int currentPage, string searchtext);
    }
}
