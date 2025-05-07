using TaskApplicationJIRA.Data;
using TaskApplicationJIRA.Models.UserModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApplicationJIRA.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Authenticate a user based on email and password
        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            return await Task.FromResult(_context.Users.FirstOrDefault(u => u.Email == email && u.Password == password));
        }

        // Register a new user
        public async Task<bool> RegisterUserAsync(User user)
        {
            if (UserExists(user.Email))
                return false;

            user.CreatedOn = DateTime.Now;
            user.CreatedBy = user.UserId;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Check if a user exists based on their email
        public bool UserExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        // Change password using email and old password (used in simpler flows)
        public async Task<bool> ChangePasswordAsync(string email, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null || user.Password != oldPassword)
            {
                return false;
            }

            user.Password = newPassword;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // ✅ Get user by UserId
        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        // ✅ Update user (for password change or other updates)
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
