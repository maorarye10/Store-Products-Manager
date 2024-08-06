using Backend.DAL.Repositories.Users;
using Backend.DTOs.User;
using Backend.Mappers;
using Backend.Services.Token;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokenService _tokenService;
        public UsersController(IUsersRepository usersRepository, ITokenService tokenService)
        {
            _usersRepository = usersRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return UnprocessableEntity(ModelState);

                var user = registerUserDTO.ToUserModel();

                var isUserExists = await _usersRepository.IsUserExistsAsync(user.Username);

                if (isUserExists)
                    return Conflict("User with that username alredy exists");

                await _usersRepository.AddUserAsync(user);

                var userDTO = user.ToUserDTO();
                var jwtToken = _tokenService.CreateToken(user);
                userDTO.token = jwtToken;
                return Ok(userDTO);

            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                    return UnprocessableEntity(ModelState);

                var user = loginUserDTO.ToUserModel();

                var loginResultUser = await _usersRepository.LoginUserAsync(user);

                if (loginResultUser == null)
                    return Unauthorized("Wrong username or password" );

                var userDTO = loginResultUser.ToUserDTO();
                var jwtToken = _tokenService.CreateToken(loginResultUser);
                userDTO.token = jwtToken;
                return Ok(new { Token = jwtToken });

            }
            catch (Exception e)
            {
                return StatusCode(500, new { Error = e.Message });
            }
        }
    }
}
