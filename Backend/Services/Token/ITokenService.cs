using Backend.Models;

namespace Backend.Services.Token
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
