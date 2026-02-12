using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Domain.RequestModel
{
    public class RevokeTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
