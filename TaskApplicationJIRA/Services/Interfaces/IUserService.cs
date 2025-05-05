// Services/UserServices/IUserService.cs
using TaskApplicationJIRA.Models.UserModel;

namespace TaskApplicationJIRA.Services.UserServices
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task CreateUserAsync(User user);
        Task UpdateUserAsync(int id, User updatedUser);
        Task DeleteUserAsync(int id);
    }
}
