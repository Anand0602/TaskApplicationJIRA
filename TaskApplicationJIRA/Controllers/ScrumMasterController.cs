// Controllers/ScrumMasterController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskApplicationJIRA.Services.ScrumMaster;

namespace TaskApplicationJIRA.Controllers
{
    [Authorize(Roles = "Scrum Master")]
    public class ScrumMasterController : Controller
    {
        private readonly IScrumMasterService _scrumMasterService;

        public ScrumMasterController(IScrumMasterService scrumMasterService)
        {
            _scrumMasterService = scrumMasterService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await _scrumMasterService.GetDashboardAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Assign(int taskId, int developerId)
        {
            await _scrumMasterService.AssignTaskAsync(taskId, developerId);
            return RedirectToAction("Index");
        }
    }
}
