using TaskApplicationJIRA.Models.UserModel;

namespace TaskApplicationJIRA.Services.AccountServices
{
    public interface IAccountService
    {
        Task<User?> AuthenticateAsync(string email, string password);
        Task<bool> RegisterUserAsync(User user);
        bool UserExists(string email);
    }
}
