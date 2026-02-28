using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.ResponseModel;

namespace TaskManagement.Repository.Interface
{
    public interface IProjectVersioning 
    {
        public Task<AboutUrgeTruckResponce> GetProjectVersion();
    }
}
