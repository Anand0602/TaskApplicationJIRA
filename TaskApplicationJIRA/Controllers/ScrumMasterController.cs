using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskApplicationJIRA.Services.ScrumMaster;
using System.Linq;
using System.Threading.Tasks;

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

        // GET: Dashboard with optional filters and sorting
        public async Task<IActionResult> Index(string statusFilter, string priorityFilter, string sortOrder)
        {
            var viewModel = await _scrumMasterService.GetDashboardAsync();

            // Filtering
            if (!string.IsNullOrEmpty(statusFilter))
                viewModel.Tasks = viewModel.Tasks.Where(t => t.Status == statusFilter).ToList();

            if (!string.IsNullOrEmpty(priorityFilter))
                viewModel.Tasks = viewModel.Tasks.Where(t => t.PriorityLevel == priorityFilter).ToList();

            // Sorting
            viewModel.Tasks = sortOrder switch
            {
                "DueDateAsc" => viewModel.Tasks.OrderBy(t => t.DueDate).ToList(),
                "DueDateDesc" => viewModel.Tasks.OrderByDescending(t => t.DueDate).ToList(),
                _ => viewModel.Tasks
            };

            return View(viewModel);
        }

        // POST: Assign Task
        [HttpPost]
        public async Task<IActionResult> Assign(int taskId, int developerId)
        {
            // Call the AssignTaskAsync method, which now returns a boolean indicating success
            var success = await _scrumMasterService.AssignTaskAsync(taskId, developerId);

            if (success)
                TempData["AssignSuccess"] = "Task assigned successfully!";
            else
                TempData["AssignError"] = "Failed to assign task. Please try again.";

            return RedirectToAction("Index");
        }
    }
}
