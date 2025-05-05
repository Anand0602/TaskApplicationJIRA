using TaskApplicationJIRA.Models.ViewModels;

namespace TaskApplicationJIRA.Services.DeveloperServices
{
    public interface IDeveloperService
    {
        Task<List<TaskAssignedDevView>> GetAssignedTasksAsync(string email);
        Task<bool> UpdateTaskStatusAsync(int taskId, string status);
    }
}
