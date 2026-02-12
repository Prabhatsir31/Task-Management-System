using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Domain.UserManagement
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
