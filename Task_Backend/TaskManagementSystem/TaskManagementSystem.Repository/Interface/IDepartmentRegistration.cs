using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.RequestModel;
using TaskManagementSystem.Domain.ResponseModel;

namespace TaskManagementSystem.Repository.Interface
{
    public interface IDepartmentRegistration
    {
        Task<List<DepartmentMasterResponse>> GetAllDepartmentAsync();
        Task<List<DepartmentMasterResponse>> GetDeptAsyncWithPagination(int skipRow, int rowSize, int currentPage, string searchtext);
        Task<List<DepartmentMasterResponse>> GetActiveDept();
        Task<ResultModel> RegisterDeptAsync(DepartmentMasterRequest deptRequest);
    }
}
