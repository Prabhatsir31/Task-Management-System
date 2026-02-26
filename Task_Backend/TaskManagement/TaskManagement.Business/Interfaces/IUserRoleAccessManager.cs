using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagement.Domain.DTOs;
using TaskManagement.Domain.ResponseModel;

namespace TaskManagement.Business.Interfaces
{
    public interface IUserRoleAccessManager
    {
        Task<UserRoleAccessDto> GetUserAccessOnRoleAsync(int roleId);
        Task<List<AllUserRoleAccessDto>> GetAllUserAccessMappingAsync();
        Task<ResultModel> AssingUserRoleAccessAsync(UserRoleAccessDto userRoleAccessDto);
    }
}
