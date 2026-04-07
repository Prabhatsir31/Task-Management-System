using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.DTOs;

namespace TaskManagement.Repository.Interface
{
    public interface IDashboard
    {
        Task<List<DashboardDto>> GetDashboardData(int UserID);
        Task<List<DepartmentTaskStatusForPieChartDto>> GetDashboardPiaData(int departmentId, int userId);
    }
}
