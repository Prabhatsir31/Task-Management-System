using System.Collections.Generic;
using TaskManagement.Domain.Common;

namespace TaskManagement.Repository.Entities
{
    public class DesignationMaster : CommonEntityModel
    {
        public DesignationMaster()
        {
            UserManager = new List<UserManager>();
        }
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<UserManager> UserManager { get; set; }
    }
}
