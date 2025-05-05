// Controllers/AdminController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskApplicationJIRA.Services.AdminServices;

namespace TaskApplicationJIRA.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminDashboardService _adminService;

        public AdminController(IAdminDashboardService adminService)
        {
            _adminService = adminService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _adminService.GetDashboardDataAsync();
            return View(viewModel);
        }
    }
}
