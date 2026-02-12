using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Domain.RequestModel
{
    public class ForgotPasswordRequest
    {
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
