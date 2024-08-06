using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.User
{
    public class RegisterUserDTO
    {
        [Required, Length(3, 20, ErrorMessage = "Username must contain between 3 to 20 characters")]
        public string Username { get; set; } = string.Empty;
        [Required, RegularExpression("(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[a-zA-Z\\d]{8,}", ErrorMessage = "Password must contain at least eight characters, one uppercase letter, one lowercase letter and one number")]
        public string Password { get; set; } = string.Empty;
    }
}
