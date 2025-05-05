// Services/Interfaces/IPriorityService.cs
using TaskApplicationJIRA.Models.PriorityModel;

namespace TaskApplicationJIRA.Services.Interfaces
{
    

    public interface IPriorityService
    {
        Task<List<Priority>> GetAllAsync();
        Task<Priority?> GetByIdAsync(int id);
        Task AddAsync(Priority priority);
        Task UpdateAsync(Priority priority);
        Task DeleteAsync(int id);
        bool Exists(int id);
    }

}
