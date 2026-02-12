using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Domain.RequestModel
{
    public class ReopenedRequest
    {
        public int TaskId { get;set;}
        public string Remarks { get;set;}
        public string ReopedBy { get; set; }
    }
}
