using TaskApplicationJIRA.ViewModels;

namespace TaskApplicationJIRA.Services.ScrumMaster
{
    public interface IScrumMasterService
    {
        Task<ScrumMasterViewModel> GetDashboardAsync();
        Task<bool> AssignTaskAsync(int taskId, int developerId);  // Return bool for success/failure
    }
}
