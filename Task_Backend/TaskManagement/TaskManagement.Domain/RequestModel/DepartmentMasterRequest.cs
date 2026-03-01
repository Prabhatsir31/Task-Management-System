using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Common;

namespace TaskManagement.Domain.RequestModel
{
    public class DepartmentMasterRequest:CommonEntityModel
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public bool IsActive { get; set; }
        public int? coordinatingIncharge { get; set; }
        public string coordinatingInchargeName { get; set; }
    }
}
