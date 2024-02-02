using System.ComponentModel.DataAnnotations;

namespace Banga.Domain.DTOs
{
    public class RegisterDto
    {
        [Required] public string UserName { get; set; }
        [Required] public string Email { get; set; }

        [Required]
        [StringLength(16,MinimumLength = 4)]
        public string Password { get; set; }
    }
}
