using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.ResponseModel;

namespace TaskManagementSystem.Repository.Interface
{
    public interface IProjectVersioning 
    {
        public Task<AboutUrgeTruckResponce> GetProjectVersion();
    }
}
