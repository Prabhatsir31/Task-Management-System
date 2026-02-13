using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.Repository.Entities
{
    public class ProjectMaster : CommonEntityModel
    {
        public ProjectMaster()
        {
            TaskTransaction = new List<TaskTransaction>();
        }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public int ManagerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public bool IsActive { get; set; }
        public virtual UserManager UserManager { get; set; }
        public virtual ICollection<TaskTransaction> TaskTransaction { get; set; }

    }
}
