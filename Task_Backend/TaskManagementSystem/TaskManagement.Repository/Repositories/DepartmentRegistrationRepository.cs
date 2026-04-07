using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common;
using TaskManagement.Domain.RequestModel;
using TaskManagement.Domain.ResponseModel;
using TaskManagement.Repository.Context;
using TaskManagement.Repository.Entities;
using TaskManagement.Repository.Interface;

namespace TaskManagement.Repository.Repositories
{
    public class DepartmentRegistrationRepository : IDepartmentRegistration
    {
        private readonly ITaskContextFactory _contextFactory;
        private readonly IMapper _mapper;

        public DepartmentRegistrationRepository(ITaskContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        
        public async Task<List<DepartmentMasterResponse>> GetAllDepartmentAsync()
        {
            //throw new NotImplementedException();
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var dept = await kUrgeTruckContext.DepartmentMaster.OrderBy(x => x.DepartmentId).ToListAsync();
            return _mapper.Map<List<DepartmentMasterResponse>>(dept);

        }

        public async Task<List<DepartmentMasterResponse>> GetDeptAsyncWithPagination(int skipRow, int rowSize, int currentPage, string searchtext)
        {
            //var skip = (currentPage - 1) * rowSize;
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            List<DepartmentMaster> dept = null;
            if (string.IsNullOrEmpty(searchtext) == false)
                dept = await kUrgeTruckContext.DepartmentMaster.Where(x => x.DepartmentName.Contains(searchtext)).OrderBy(x => x.DepartmentName).Skip(skipRow).Take(rowSize).ToListAsync();
            else 
                dept = await kUrgeTruckContext.DepartmentMaster.OrderBy(x=>x.DepartmentName).Skip(skipRow).Take(rowSize).ToListAsync();
            //roles = await kUrgeTruckContext.RoleMaster.Where(x => x.IsActive == true).ToListAsync();
            var response = _mapper.Map<List<DepartmentMasterResponse>>(dept);
            if (response.Count > 0 && !string.IsNullOrEmpty(searchtext))
                response[0].TotalRecord = await kUrgeTruckContext.DepartmentMaster.Where(x => x.DepartmentName.Contains(searchtext)).CountAsync();
            else if (response.Count > 0)
                response[0].TotalRecord = await kUrgeTruckContext.DepartmentMaster.CountAsync();
            return response;
        }

        public async Task<List<DepartmentMasterResponse>> GetActiveDept()
        {
            //throw new NotImplementedException();
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var dept = await kUrgeTruckContext.DepartmentMaster.OrderBy(x => x.DepartmentId).Where(x=>x.IsActive).ToListAsync();
            return _mapper.Map<List<DepartmentMasterResponse>>(dept);
        }

        public async Task<ResultModel> RegisterDeptAsync(DepartmentMasterRequest deptRequest)
        {
            var resMessage = "Department ";
            try
            {
                using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var departmentExists = kUrgeTruckContext.DepartmentMaster.Any(x => x.DepartmentName == deptRequest.DepartmentName && x.DepartmentId != deptRequest.DepartmentId);
                if (departmentExists == true)
                {
                    return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, TaskMessages.dept_Exist);
                }
                var dept = await kUrgeTruckContext.DepartmentMaster
                                                  .FirstOrDefaultAsync(x => x.DepartmentId == deptRequest.DepartmentId || x.DepartmentName == deptRequest.DepartmentName);
                if (dept == null)
                {
                    var department = _mapper.Map<DepartmentMaster>(deptRequest);
                    kUrgeTruckContext.Add(department);
                    resMessage = resMessage + TaskMessages.added_successfully;
                    await kUrgeTruckContext.SaveChangesAsync();
                    return ResultModelFactory.CreateSucess(resMessage);
                }
                else
                {
                    if (deptRequest.IsActive == false)
                    {
                        var userAssign = await kUrgeTruckContext.UserManager.Where(x => x.DepartmentId == dept.DepartmentId && x.IsActive).CountAsync();
                        if (userAssign > 0)
                            return ResultModelFactory.CreateFailure(ResultCode.Invalid, TaskMessages.dept_Assign_to_User);
                    }
                    dept.DepartmentName = deptRequest.DepartmentName;
                    dept.IsActive = deptRequest.IsActive;
                    dept.ModifiedBy = deptRequest.ModifiedBy;
                    dept.ModifiedDate = deptRequest.ModifiedDate;
                    dept.coordinatingIncharge = deptRequest.coordinatingIncharge;
                    dept.coordinatingInchargeName = deptRequest.coordinatingInchargeName;
                    resMessage = resMessage + TaskMessages.updated_successfully;
                    kUrgeTruckContext.DepartmentMaster.Update(dept);
                    await kUrgeTruckContext.SaveChangesAsync();
                    return ResultModelFactory.UpdateSucess(resMessage);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error while register department " + ex);
                return ResultModelFactory.CreateFailure(ResultCode.ExceptionThrown, TaskMessages.Error_while_addupdate_dept, ex);
            }
        }
    }
}
