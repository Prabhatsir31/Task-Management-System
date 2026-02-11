using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.DTOs;
using TaskManagementSystem.Domain.ResponseModel;

namespace TaskManagementSystem.Business.Interfaces
{
    public interface IUserRoleAccessManager
    {
        Task<UserRoleAccessDto> GetUserAccessOnRoleAsync(int roleId);
        Task<List<AllUserRoleAccessDto>> GetAllUserAccessMappingAsync();
        Task<ResultModel> AssingUserRoleAccessAsync(UserRoleAccessDto userRoleAccessDto);
    }
}
