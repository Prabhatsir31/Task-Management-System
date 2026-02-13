using TaskManagementSystem.Domain.Common;
using TaskManagementSystem.Domain.ResponseModel;
using TaskManagementSystem.Repository.Interface;

namespace TaskManagementSystem.Repository.Repositories
{
    public class ProjectVersionRepository : IProjectVersioning
    {
        public async Task<AboutUrgeTruckResponce> GetProjectVersion()
        {
            AboutUrgeTruckResponce ProjectCurrentVersion = new AboutUrgeTruckResponce();
            ProjectCurrentVersion.ProjectCurrentVersion = UrgeTruckVersion.ProjectCurrentVersion;
            return ProjectCurrentVersion;
        }
    }
}
