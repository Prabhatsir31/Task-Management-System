using System.Collections.Generic;
using TaskManagement.Domain.Common;

namespace TaskManagement.Repository.Entities
{
    public class TaskTypeMaster : CommonEntityModel
    {
        public TaskTypeMaster()
        {
            TaskTransaction = new List<TaskTransaction>();
        }
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public bool IsActive { get; set; }
        public string NextTaskname { get; set; }
        public int? NextTaskId { get; set; }
        public int? DepartmentId { get; set; }
        public virtual DepartmentMaster DepartmentMaster { get; set; }
        public virtual ICollection<TaskTransaction> TaskTransaction { get; set; }
    }
}
