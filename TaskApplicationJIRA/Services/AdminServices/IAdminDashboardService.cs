// Services/AdminServices/IAdminDashboardService.cs
using TaskApplicationJIRA.Models.ViewModels;

namespace TaskApplicationJIRA.Services.AdminServices
{
    public interface IAdminDashboardService
    {
        Task<AdminDashboardViewModel> GetDashboardDataAsync();
    }
}
