using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.Domain.RequestModel
{
    public class DesignationMasterRequest:CommonEntityModel
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public bool IsActive { get; set; }
        public int TotalRecord { get; set; }
    }
}
