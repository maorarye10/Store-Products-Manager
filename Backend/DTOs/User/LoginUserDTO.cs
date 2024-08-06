using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.User
{
    public class LoginUserDTO
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
