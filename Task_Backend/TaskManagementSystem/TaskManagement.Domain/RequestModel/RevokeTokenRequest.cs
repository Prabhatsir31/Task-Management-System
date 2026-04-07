using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Domain.RequestModel
{
    public class RevokeTokenRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
