using Backend.DTOs.User;
using Backend.Models;

namespace Backend.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToUserDTO(this User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                LastLogin = user.LastLogin,
            };
        }
        public static User ToUserModel(this RegisterUserDTO userDTO)
        {
            return new User
            {
                Username = userDTO.Username,
                Password = userDTO.Password
            };
        }

        public static User ToUserModel(this LoginUserDTO userDTO)
        {
            return new User
            {
                Username = userDTO.Username,
                Password = userDTO.Password
            };
        }
    }
}
