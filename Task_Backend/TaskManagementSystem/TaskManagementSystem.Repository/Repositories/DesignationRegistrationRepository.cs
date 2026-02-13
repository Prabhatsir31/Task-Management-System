using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Repository.Context;
using TaskManagementSystem.Repository.Interface;
using TaskManagementSystem.Domain.RequestModel;
using TaskManagementSystem.Domain.Common;
using TaskManagementSystem.Domain.ResponseModel;
using TaskManagementSystem.Repository.Entities;

namespace TaskManagementSystem.Repository.Repositories
{
    public class DesignationRegistrationRepository : IDesignationRegistration
    {
        private readonly ITaskContextFactory _contextFactory;
        private readonly IMapper _mapper;

        public DesignationRegistrationRepository(ITaskContextFactory contextFactory, IMapper mapper)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
        }

        public async Task<List<DesignationMasterResponse>> GetAllDeisgnationAsync()
        {
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var dept = await kUrgeTruckContext.DesignationMaster.OrderBy(x => x.DesignationId).ToListAsync();
            return _mapper.Map<List<DesignationMasterResponse>>(dept);
        }

        public async Task<List<DesignationMasterResponse>> GetDesignationAsyncWithPagination(int skipRow, int rowSize, int currentPage, string searchtext)
        {
            //var skip = (currentPage - 1) * rowSize;
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            List<DesignationMaster> designation = null;
            if (string.IsNullOrEmpty(searchtext) == false)
                designation = await kUrgeTruckContext.DesignationMaster.Where(x => x.DesignationName.Contains(searchtext)).OrderBy(x => x.DesignationName).Skip(skipRow).Take(rowSize).ToListAsync();
            else
                designation = await kUrgeTruckContext.DesignationMaster.OrderBy(x => x.DesignationName).Skip(skipRow).Take(rowSize).ToListAsync();
            //roles = await kUrgeTruckContext.RoleMaster.Where(x => x.IsActive == true).ToListAsync();
            var response = _mapper.Map<List<DesignationMasterResponse>>(designation);
            if (response.Count > 0 && string.IsNullOrEmpty(searchtext))
                response[0].TotalRecord = await kUrgeTruckContext.DesignationMaster.CountAsync();
            else if (response.Count > 0)
                response[0].TotalRecord = designation.Count();
            return response;
        }

        public async Task<List<DesignationMasterResponse>> GetActiveDesignation()
        {
            //throw new NotImplementedException();
            using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
            var designation = await kUrgeTruckContext.DesignationMaster.OrderBy(x => x.DesignationId).Where(x => x.IsActive).ToListAsync();
            return _mapper.Map<List<DesignationMasterResponse>>(designation);
        }

        public async Task<ResultModel> RegisterDesignationAsync(DesignationMasterRequest designationRequest)
        {
            var resMessage = "Designation ";
            try
            {
                using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var designationExists = kUrgeTruckContext.DesignationMaster.Any(x => x.DesignationName == designationRequest.DesignationName && x.DesignationId != designationRequest.DesignationId);
                if (designationExists == true)
                {
                    return ResultModelFactory.CreateFailure(ResultCode.DuplicateRecord, UrgeTruckMessages.designation_Exist);
                }
                var designation = await kUrgeTruckContext.DesignationMaster
                                                  .FirstOrDefaultAsync(x => x.DesignationId == designationRequest.DesignationId || x.DesignationName == designationRequest.DesignationName);
                if (designation == null)
                {
                    var design = _mapper.Map<DesignationMaster>(designationRequest);
                    kUrgeTruckContext.Add(design);
                    resMessage = resMessage + UrgeTruckMessages.added_successfully;
                    await kUrgeTruckContext.SaveChangesAsync();
                    designationRequest.DesignationId = design.DesignationId;
                    return ResultModelFactory.CreateSucess(resMessage);
                }
                else
                {
                    if (designationRequest.IsActive == false)
                    {
                        var userAssign = await kUrgeTruckContext.UserManager.Where(x => x.DesignationId == designation.DesignationId && x.IsActive).CountAsync();
                        if (userAssign > 0)
                            return ResultModelFactory.CreateFailure(ResultCode.Invalid, UrgeTruckMessages.design_Assign_to_User);
                    }
                    designation.DesignationName = designationRequest.DesignationName;
                    designation.IsActive = designationRequest.IsActive;
                    designation.ModifiedBy = designationRequest.ModifiedBy;
                    designation.ModifiedDate = designationRequest.ModifiedDate;
                    resMessage = resMessage + UrgeTruckMessages.updated_successfully;
                    kUrgeTruckContext.DesignationMaster.Update(designation);
                    await kUrgeTruckContext.SaveChangesAsync();
                    return ResultModelFactory.UpdateSucess(resMessage);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error while register designation " + ex);
                return ResultModelFactory.CreateFailure(ResultCode.ExceptionThrown, UrgeTruckMessages.Error_while_addupdate_designation, ex);
            }
        }
    }
}
