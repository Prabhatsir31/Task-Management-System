using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Domain.UserManagement
{
    public class ForgotPassword
    {
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
    }
}
