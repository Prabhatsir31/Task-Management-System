using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Domain.RequestModel
{
    public class AuthenticateRequest
    {
        [Required]
        [MinLength(5)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
