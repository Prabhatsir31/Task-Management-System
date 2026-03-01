using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.RequestModel
{
    public class ForgotPasswordRequest
    {
        [Required]
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
