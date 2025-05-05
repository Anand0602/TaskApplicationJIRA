// Services/ScrumMaster/IScrumMasterService.cs
using TaskApplicationJIRA.ViewModels;

namespace TaskApplicationJIRA.Services.ScrumMaster
{
    public interface IScrumMasterService
    {
        Task<ScrumMasterViewModel> GetDashboardAsync();
        Task AssignTaskAsync(int taskId, int developerId);
    }
}
