using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.RequestModel
{
    public class ChangePasswordRequest
    {
        [Required]
        public string UserName { get; set; }
        
        [Required]
        [MinLength(6)]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
