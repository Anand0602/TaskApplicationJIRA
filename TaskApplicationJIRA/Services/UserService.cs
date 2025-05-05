// Services/UserServices/UserService.cs
using Microsoft.EntityFrameworkCore;
using TaskApplicationJIRA.Data;
using TaskApplicationJIRA.Models.UserModel;

namespace TaskApplicationJIRA.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task CreateUserAsync(User user)
        {
            user.CreatedOn = DateTime.Now;
            user.CreatedBy = 1;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(int id, User updatedUser)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return;

            user.Username = updatedUser.Username;
            user.Email = updatedUser.Email;
            user.Role = updatedUser.Role;
            user.Password = updatedUser.Password;
            user.UpdatedOn = DateTime.Now;
            user.UpdatedBy = 1;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return;

            user.DeletedOn = DateTime.Now;
            user.DeletedBy = 1;
            _context.Users.Remove(user); // or soft-delete
            await _context.SaveChangesAsync();
        }
    }
}
