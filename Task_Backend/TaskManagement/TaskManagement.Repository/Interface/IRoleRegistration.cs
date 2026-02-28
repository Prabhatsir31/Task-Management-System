using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Domain.RequestModel;
using TaskManagement.Domain.ResponseModel;

namespace TaskManagement.Repository.Interface
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
