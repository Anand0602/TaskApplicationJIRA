using TaskApplicationJIRA.Models.ViewModels;
using TaskApplicationJIRA.Models.TaskModel;

namespace TaskApplicationJIRA.Services.Interfaces
{
    

    public interface ITaskService
    {
        Task<TaskItemViewModel> GetCreateTaskViewModelAsync();
        Task CreateTaskAsync(TaskItemViewModel model, IFormFile image);
        Task<TaskItemViewModel?> GetEditTaskViewModelAsync(int id);
        Task<bool> EditTaskAsync(int id, TaskItemViewModel model, IFormFile image);
        Task<TaskItem?> GetTaskForDeleteAsync(int id);
        Task DeleteTaskAsync(int id);
    }

}
