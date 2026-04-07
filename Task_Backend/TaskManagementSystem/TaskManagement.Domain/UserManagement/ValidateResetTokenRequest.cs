using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.UserManagement
{
    public class ValidateResetTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
