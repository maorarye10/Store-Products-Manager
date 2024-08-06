using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.DAL.Repositories.Users
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDBContext _context;
        public UsersRepository(AppDBContext context)
        {
            _context = context;
        }
        public async Task<User> AddUserAsync(User user)
        {
            user.LastLogin = DateTime.Now;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> LoginUserAsync(User user)
        {
            var resultUser = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(user.Username));

            if (resultUser == null)
                return null;

            if (!resultUser.Password.Equals(user.Password))
                return null;

            resultUser.LastLogin = DateTime.Now;
            return resultUser;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            return user;
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.Equals(username));
            return user;
        }

        public async Task<bool> IsUserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username.Equals(username));
        }

        public async Task<User?> PromoteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                user.Role = "admin";
                await _context.SaveChangesAsync();
            }

            return user;
        }

        public async Task DemoteUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                user.Role = "user";
                await _context.SaveChangesAsync();
            }
        }
    }
}
