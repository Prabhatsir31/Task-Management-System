using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Common;

namespace TaskManagementSystem.Domain.RequestModel
{
    public class NotificationRequest : CommonEntityModel
    {
        public int NotificationId { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
