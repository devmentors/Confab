using System.ComponentModel.DataAnnotations;

namespace Confab.Modules.Users.Core.DTO
{
    public class SignInDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}