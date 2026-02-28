using System.Threading.Tasks;
using TaskManagement.Repository.Interface;
using TaskManagement.Domain.ResponseModel;
using TaskManagement.Domain.Common;

namespace TaskManagement.Repository.Repositories
{
    public class ProjectVersionRepository : IProjectVersioning
    {
        public async Task<AboutUrgeTruckResponce> GetProjectVersion()
        {
            AboutUrgeTruckResponce ProjectCurrentVersion = new AboutUrgeTruckResponce();
            ProjectCurrentVersion.ProjectCurrentVersion = TaskVersion.ProjectCurrentVersion;
            return ProjectCurrentVersion;
        }
    }
}
