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
    public class UserAccessManagerRepository : IUserAccessManager
    {
        private readonly ITaskContextFactory _contextFactory;
        private readonly IMapper _mapper;

        public UserAccessManagerRepository(ITaskContextFactory contextFactory,
            IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<UserAccessManagerResponse>> GetAllActiveUserAccessAsync()
        {
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var userAccessList = await kUrgeTruckContext.UserAccessManager.Where(x => x.IsActive == true).ToListAsync();
            return _mapper.Map<List<UserAccessManagerResponse>>(userAccessList);
        }

        public async Task<List<UserAccessManagerResponse>> GetAllUserAccessAsync()
        {
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var userAccessList = await kUrgeTruckContext.UserAccessManager
                                                        .Include(x => x.UserScreenMaster)
                                                        .Include(x => x.RoleMaster)
                                                        .ToListAsync();
            return _mapper.Map<List<UserAccessManagerResponse>>(userAccessList);
        }

        public async Task<UserAccessManagerResponse> GetUserAccessAsync(int accessManagerId)
        {
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var userAccess = await kUrgeTruckContext.UserAccessManager.FirstOrDefaultAsync(x => x.UserAccessManagerId == accessManagerId);
            return _mapper.Map<UserAccessManagerResponse>(userAccess);
        }

        public async Task<List<UserAccessManagerResponse>> GetUserAccessBasedOnRoleIdAsync(int roleId)
        {
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var userAccess = await kUrgeTruckContext.UserAccessManager
                                                    .Include(x => x.UserScreenMaster)
                                                    .Include(x => x.RoleMaster)
                                                    .Where(x => x.RoleId == roleId).ToListAsync();
            return _mapper.Map<List<UserAccessManagerResponse>>(userAccess);
        }

        public async Task<ResultModel> RegisterUserAccessAsync(List<UserAccessManagerRequest> requestModel)
        {
            var resMessage = UrgeTruckMessages.User_access_assinged_successfully;
            try
            {
                using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                foreach (var currentRequest in requestModel)
                {
                    var userAccessManager = await kUrgeTruckContext.UserAccessManager
                                                 .FirstOrDefaultAsync(x => x.UserAccessManagerId == currentRequest.UserAccessManagerId || x.RoleId == currentRequest.RoleId && x.UserScreenId == currentRequest.UserScreenId);
                    if (userAccessManager == null)
                    {
                        kUrgeTruckContext.Add(_mapper.Map<UserAccessManager>(currentRequest));
                    }
                    else
                    {
                        userAccessManager.CanCreate = currentRequest.CanCreate;
                        userAccessManager.CanUpdate = currentRequest.CanUpdate;
                        userAccessManager.CanDeactivate = currentRequest.CanDeactivate;
                        userAccessManager.IsActive = currentRequest.IsActive;
                        userAccessManager.RoleId = currentRequest.RoleId;
                        userAccessManager.UserScreenId = currentRequest.UserScreenId;
                        kUrgeTruckContext.UserAccessManager.Update(userAccessManager);
                    }
                }
                await kUrgeTruckContext.SaveChangesAsync();
                return ResultModelFactory.CreateSucess(resMessage);
            }
            catch (Exception ex)
            {
                Logger.Error("Error while register user access " + ex);
                return ResultModelFactory.CreateFailure(ResultCode.ExceptionThrown, UrgeTruckMessages.Error_while_addupdate_user_access, ex);
            }
        }

        public async Task<List<UserResponse>> GetAllUserbyDepartmentAsync(int department)
        {
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            List<UserResponse> users = new List<UserResponse>();
            var user = await kUrgeTruckContext.UserManager.Where(x => x.DepartmentId == department).ToListAsync();
            users = _mapper.Map<List<UserResponse>>(user);
            return users;
        }
    }
}
