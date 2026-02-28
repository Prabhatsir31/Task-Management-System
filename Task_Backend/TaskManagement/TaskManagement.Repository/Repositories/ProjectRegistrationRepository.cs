using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Repository.Context;
using TaskManagement.Repository.Interface;
using TaskManagement.Domain.RequestModel;
using TaskManagement.Domain.ResponseModel;
using TaskManagement.Domain.Common;
using TaskManagement.Repository.Entities;

namespace TaskManagement.Repository.Repositories
{
    public class ProjectRegistrationRepository : IProjectRegistration
    {
        private readonly ITaskContextFactory _contextFactory;
        private readonly IMapper _mapper;

        public ProjectRegistrationRepository(ITaskContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }


        public async Task<List<ProjectMasterResponse>> GetAllProjectAsync()
        {
            //throw new NotImplementedException();
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var project = await kUrgeTruckContext.ProjectMaster.OrderByDescending(x => x.ProjectId).ToListAsync();
            return _mapper.Map<List<ProjectMasterResponse>>(project);
        }

        public async Task<List<ProjectMasterResponse>> GetActiveProjectAsync()
        {
            //using KUrgeTruckContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            //var project = await kUrgeTruckContext.ProjectMaster.OrderByDescending(x => x.ProjectId).Where(x => x.IsActive).ToListAsync();
            //return _mapper.Map<List<ProjectMasterResponse>>(project);


            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            //var userList = await kUrgeTruckContext.UserManager.ToListAsync();
            var userList = await kUrgeTruckContext.ProjectMaster.OrderByDescending(x => x.ProjectId).Where(x => x.IsActive).ToListAsync();
            return _mapper.Map<List<ProjectMasterResponse>>(userList);
        }
        public async Task<List<UserResponse>> GetManagerDDLAsync()
        {
            try
            {
                using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var ManagerList = await kUrgeTruckContext.UserManager.OrderBy(x => x.FirstName).Where(x => x.IsActive).ToListAsync();

                return _mapper.Map<List<UserResponse>>(ManagerList);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public async Task<List<ProjectMasterResponse>> GetProjectAsyncWithPagination(int skipRow, int rowSize, int currentPage, string searchtext, string status)
        {
            try
            {
                //var skip = (currentPage - 1) * rowSize;
                using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<ProjectMaster> projectMaster = null;
                if (string.IsNullOrEmpty(searchtext) == false && status == "0" && string.IsNullOrEmpty(status))//when searchtext is not null
                    projectMaster = await kUrgeTruckContext.ProjectMaster.Include(x => x.UserManager).Where(x => x.ProjectName.Contains(searchtext)).OrderByDescending(x => x.Status == TaskTransactionStatus.Ongoing).Skip(skipRow).Take(rowSize).ToListAsync();

                if (!string.IsNullOrEmpty(status) && status != "0" && string.IsNullOrEmpty(searchtext))//when status is not null
                    projectMaster = await kUrgeTruckContext.ProjectMaster.Include(x => x.UserManager).Where(x => x.Status == status).OrderByDescending(x => x.Status == TaskTransactionStatus.Ongoing).Skip(skipRow).Take(rowSize).ToListAsync();

                if (!string.IsNullOrEmpty(status) && status != "0" && !string.IsNullOrEmpty(searchtext))//when searchtext and status is not null
                    projectMaster = await kUrgeTruckContext.ProjectMaster.Include(x => x.UserManager).Where(x => x.Status == status && x.ProjectName.Contains(searchtext)).OrderByDescending(x => x.Status == TaskTransactionStatus.Ongoing).Skip(skipRow).Take(rowSize).ToListAsync();

                if (string.IsNullOrEmpty(searchtext) && string.IsNullOrEmpty(status)) //when both are nullorempty
                {
                    projectMaster = await kUrgeTruckContext.ProjectMaster.Include(x => x.UserManager).OrderByDescending(x => x.Status == TaskTransactionStatus.Ongoing).Skip(skipRow).Take(rowSize).ToListAsync();
                }
                var response = _mapper.Map<List<ProjectMasterResponse>>(projectMaster);
                if (response.Count > 0 && !string.IsNullOrEmpty(searchtext) && status == "0" && string.IsNullOrEmpty(status))//when searchtext is not null
                    response[0].TotalRecord = await kUrgeTruckContext.ProjectMaster.Include(x => x.UserManager).Where(x => x.ProjectName.Contains(searchtext)).CountAsync();
                else if (response.Count > 0 && string.IsNullOrEmpty(searchtext) && status != "0" && !string.IsNullOrEmpty(status)) //when status is not null
                    response[0].TotalRecord = await kUrgeTruckContext.ProjectMaster.Include(x => x.UserManager).Where(x => x.Status == status).CountAsync();
                else if (response.Count > 0 && !string.IsNullOrEmpty(searchtext) && status != "0" && !string.IsNullOrEmpty(status)) //when searchtext and status is not null
                    response[0].TotalRecord = await kUrgeTruckContext.ProjectMaster.Include(x => x.UserManager).Where(x => x.ProjectName.Contains(searchtext) && x.Status == status).CountAsync();
                else if (response.Count > 0)
                    response[0].TotalRecord = await kUrgeTruckContext.ProjectMaster.Include(x => x.UserManager).CountAsync();
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ResultModel> RegisterProjectAsync(ProjectMasterRequest projectRequest)
        {
            var resMessage = "Project ";
            try
            {
                using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var projectExists = kUrgeTruckContext.ProjectMaster.Any(x => x.ProjectName == projectRequest.ProjectName && x.ProjectId != projectRequest.ProjectId);
                if (projectExists == true)
                {
                    return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, TaskMessages.project_Exist);
                }
                var project = await kUrgeTruckContext.ProjectMaster
                                                  .FirstOrDefaultAsync(x => x.ProjectId == projectRequest.ProjectId || x.ProjectName == projectRequest.ProjectName);
                if (project == null)
                {
                    var proj = _mapper.Map<ProjectMaster>(projectRequest);
                    kUrgeTruckContext.Add(proj);
                    resMessage = resMessage + TaskMessages.added_successfully;
                    await kUrgeTruckContext.SaveChangesAsync();
                    projectRequest.ProjectId = proj.ProjectId;
                    return ResultModelFactory.CreateSucess(resMessage);
                }
                else
                {
                    project.ProjectName = projectRequest.ProjectName;
                    project.Status = projectRequest.Status;
                    project.Description = projectRequest.Description;
                    project.Remark = projectRequest.Remark;
                    project.ManagerId = projectRequest.ManagerId;
                    project.StartDate = projectRequest.StartDate;
                    project.EndDate = projectRequest.EndDate;
                    project.IsActive = projectRequest.IsActive;
                    project.ModifiedBy = projectRequest.ModifiedBy;
                    project.ModifiedDate = projectRequest.ModifiedDate;
                    resMessage = resMessage + TaskMessages.updated_successfully;
                    kUrgeTruckContext.ProjectMaster.Update(project);
                    await kUrgeTruckContext.SaveChangesAsync();
                    return ResultModelFactory.UpdateSucess(resMessage);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error while register project " + ex);
                return ResultModelFactory.CreateFailure(ResultCode.ExceptionThrown, TaskMessages.Error_while_addupdate_project, ex);
            }
        }


    }
}
