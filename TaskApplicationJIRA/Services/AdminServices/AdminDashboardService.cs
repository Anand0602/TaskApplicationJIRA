// Services/AdminServices/AdminDashboardService.cs
using Microsoft.EntityFrameworkCore;
using TaskApplicationJIRA.Data;
using TaskApplicationJIRA.Models.ViewModels;

namespace TaskApplicationJIRA.Services.AdminServices
{
    public class AdminDashboardService : IAdminDashboardService
    {
        private readonly ApplicationDbContext _context;

        public AdminDashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdminDashboardViewModel> GetDashboardDataAsync()
        {
            var users = await _context.Users.ToListAsync();
            var tasks = await _context.Tasks.ToListAsync();
            var categories = await _context.Categories.ToListAsync();
            var priorities = await _context.Priorities.ToListAsync();

            return new AdminDashboardViewModel
            {
                Users = users,
                Tasks = tasks,
                Categories = categories,
                Priorities = priorities
            };
        }
    }
}
