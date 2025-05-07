using TaskApplicationJIRA.Models.UserModel;
using System.Threading.Tasks;

namespace TaskApplicationJIRA.Services.AccountServices
{
    public interface IAccountService
    {
        // Method to authenticate a user
        Task<User?> AuthenticateAsync(string email, string password);

        // Method to register a new user
        Task<bool> RegisterUserAsync(User user);

        // Method to check if a user exists
        bool UserExists(string email);

        // Method to change the user's password
        Task<bool> ChangePasswordAsync(string email, string oldPassword, string newPassword);

        Task<User?> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(User user);

    }
}
