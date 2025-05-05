using TaskApplicationJIRA.Data;
using TaskApplicationJIRA.Models.UserModel;

namespace TaskApplicationJIRA.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            return await Task.FromResult(_context.Users.FirstOrDefault(u => u.Email == email && u.Password == password));
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            if (UserExists(user.Email))
                return false;

            if (user.Email == "admin@gmail.com")
                user.Role = "Admin";

            user.CreatedOn = DateTime.Now;
            user.CreatedBy = user.UserId;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public bool UserExists(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}
