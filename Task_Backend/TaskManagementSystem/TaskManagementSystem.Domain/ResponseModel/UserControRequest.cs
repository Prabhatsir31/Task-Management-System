using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.RequestModel;

namespace TaskManagementSystem.Domain.ResponseModel
{
   public class UserControRequest
    {

            public UserControRequest()
            {
            UserAccessManagerRequest = new List<UserAccessManagerRequest>();
            }
            public int RoleId { get; set; }
            

            public virtual List<UserAccessManagerRequest> UserAccessManagerRequest { get; set; }
        
    }
}
