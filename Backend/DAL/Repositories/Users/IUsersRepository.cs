using Backend.Models;

namespace Backend.DAL.Repositories.Users
{
    public interface IUsersRepository
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User> AddUserAsync(User user);
        Task<User?> PromoteUserAsync(int  id);
        Task DemoteUserAsync(int id);
        Task<User?> LoginUserAsync(User user);
        Task<bool> IsUserExistsAsync(string username);

    }
}
