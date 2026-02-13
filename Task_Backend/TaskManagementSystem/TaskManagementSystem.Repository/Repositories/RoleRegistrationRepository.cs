using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain.Common;
using TaskManagementSystem.Domain.RequestModel;
using TaskManagementSystem.Domain.ResponseModel;
using TaskManagementSystem.Repository.Context;
using TaskManagementSystem.Repository.Entities;
using TaskManagementSystem.Repository.Interface;

namespace TaskManagementSystem.Repository.Repositories
{
    public class RoleRegistrationRepository : IRoleRegistration
    {
        private readonly ITaskContextFactory _contextFactory;
        private readonly IMapper _mapper;

        public RoleRegistrationRepository(ITaskContextFactory contextFactory,
            IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<RoleMasterResponse>> GetAllActiveRolesAsync()
        {
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var roles = await kUrgeTruckContext.RoleMaster.Where(x => x.IsActive == true).ToListAsync();
            return _mapper.Map<List<RoleMasterResponse>>(roles);
        }

        public async Task<List<RoleMasterResponse>> GetAllRolesAsync()
        {
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var roles = await kUrgeTruckContext.RoleMaster.OrderBy(x => x.RoleId).ToListAsync();
            return _mapper.Map<List<RoleMasterResponse>>(roles);
        }

        public async Task<RoleMasterResponse> GetRoleAsync(int roleId, string roleName = null)
        {
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var role = await kUrgeTruckContext.RoleMaster.FirstOrDefaultAsync(x => x.RoleId == roleId || x.RoleName == roleName);
            return _mapper.Map<RoleMasterResponse>(role);
        }

        public async Task<ResultModel> RegisterRoleAsync(RoleMasterRequest request)
        {
            var resMessage = "Role ";
            try
            {
                using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var userRoleExists = kUrgeTruckContext.RoleMaster.Any(x => x.RoleName == request.RoleName && x.RoleId != request.RoleId);
                if (userRoleExists == true)
                {
                    return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, UrgeTruckMessages.role_Exist);
                }
                var role = await kUrgeTruckContext.RoleMaster
                                                  .FirstOrDefaultAsync(x => x.RoleId == request.RoleId || x.RoleName == request.RoleName);
                if (role == null)
                {
                    var newRole = _mapper.Map<RoleMaster>(request);
                    kUrgeTruckContext.Add(newRole);
                    resMessage = resMessage + UrgeTruckMessages.added_successfully;
                    await kUrgeTruckContext.SaveChangesAsync();
                    request.RoleId = newRole.RoleId;
                    return ResultModelFactory.CreateSucess(resMessage);
                }
                else
                {
                    if (request.IsActive == false)
                    {
                        var userAssign = await kUrgeTruckContext.UserManager.Where(x => x.RoleId == role.RoleId && x.IsActive).CountAsync();
                        if (userAssign > 0)
                            return ResultModelFactory.CreateFailure(ResultCode.Invalid, UrgeTruckMessages.role_Assign_to_User);
                    }
                    role.RoleName = request.RoleName;
                    role.IsActive = request.IsActive;
                    role.RoleGroup = request.RoleGroup;
                    resMessage = resMessage + UrgeTruckMessages.updated_successfully;
                    kUrgeTruckContext.RoleMaster.Update(role);
                    await kUrgeTruckContext.SaveChangesAsync();
                    return ResultModelFactory.UpdateSucess(resMessage);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error while register role " + ex);
                return ResultModelFactory.CreateFailure(ResultCode.ExceptionThrown, UrgeTruckMessages.Error_while_addupdate_role, ex);
            }
        }

        public async Task<List<RoleMasterResponse>> GetRoleAsyncWithPagination( int rowSize, int currentPage, string searchtext)
        {
            var skip = (currentPage - 1) * rowSize;
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            List<RoleMaster> roles = null;
            if (string.IsNullOrEmpty(searchtext) == false)
                roles = await kUrgeTruckContext.RoleMaster.Where(x => x.RoleName.Contains(searchtext)).OrderBy(x => x.RoleName).Skip(skip).Take(rowSize).ToListAsync();
            else
                roles = await kUrgeTruckContext.RoleMaster.OrderBy(x => x.RoleName).Skip(skip).Take(rowSize).ToListAsync();
            //roles = await kUrgeTruckContext.RoleMaster.Where(x => x.IsActive == true).ToListAsync();
            var response = _mapper.Map<List<RoleMasterResponse>>(roles);
            if (response.Count > 0 && string.IsNullOrEmpty(searchtext))
                response[0].TotalRecord = await kUrgeTruckContext.RoleMaster.CountAsync();
            else if (response.Count > 0)
                response[0].TotalRecord = roles.Count();
            return response;
        }
    }
}
