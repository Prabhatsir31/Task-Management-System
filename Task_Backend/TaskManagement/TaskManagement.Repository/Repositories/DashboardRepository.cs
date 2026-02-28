using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.DTOs;
using TaskManagement.Repository.Context;
using TaskManagement.Repository.Interface;

namespace TaskManagement.Repository.Repositories
{
    public class DashboardRepository : IDashboard
    {
        private readonly ITaskContextFactory _contextFactory;
        private readonly IMapper _mapper;
        private readonly ITaskTransaction _transaction;
        public DashboardRepository(ITaskContextFactory contextFactory, IMapper mapper, ITaskTransaction transaction)
        {
            _contextFactory = contextFactory;
            _mapper = mapper;
            _transaction = transaction;
        }

        public async Task<List<DashboardDto>> GetDashboardData(int userId)
        {
            try
            {
                using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                List<DashboardDto> dashData = new List<DashboardDto>();
                var parameters = new List<SqlParameter>();
                //parameters.Add(new SqlParameter("@date", fromDate ?? (object)DBNull.Value));
                parameters.Add(new SqlParameter("@userId", userId));
                dashData = await kUrgeTruckContext.DashboardDto.FromSqlRaw("dashboardData @userId", parameters.ToArray()).ToListAsync();

                var departmentStatusData = await kUrgeTruckContext.DepartmentStatus.FromSqlRaw("DepartmentStatus").ToListAsync();
                if (dashData.Count > 0 && departmentStatusData.Count > 0)
                    dashData[0].departmentTaskStatuses = departmentStatusData;
                return dashData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<DepartmentTaskStatusForPieChartDto>> GetDashboardPiaData(int departmentId, int userId)
        {
            try
            {
                using TaskContext kUrgeTruckContext = _contextFactory.CreateKGASContext();
                var parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@departmentId", departmentId));
                parameters.Add(new SqlParameter("@userId", userId));
                var departmentStatusData = await kUrgeTruckContext.DepartmentTaskStatusForPieChart.FromSqlRaw("DashboardPieChartData @departmentId, @userId",parameters.ToArray()).ToListAsync();
                return departmentStatusData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



    }
}
