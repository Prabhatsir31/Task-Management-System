using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.RequestModel;
using TaskManagementSystem.Domain.ResponseModel;

namespace TaskManagementSystem.Repository.Interface
{
    public interface IDesignationRegistration
    {
        Task<List<DesignationMasterResponse>> GetAllDeisgnationAsync();
        Task<List<DesignationMasterResponse>> GetDesignationAsyncWithPagination(int skipRow, int rowSize, int currentPage, string searchtext);
        Task<List<DesignationMasterResponse>> GetActiveDesignation();
        Task<ResultModel> RegisterDesignationAsync(DesignationMasterRequest designationRequest);
    }
}
