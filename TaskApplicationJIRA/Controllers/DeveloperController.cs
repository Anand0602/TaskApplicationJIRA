// Controllers/DeveloperController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskApplicationJIRA.Services.DeveloperServices;

namespace TaskApplicationJIRA.Controllers
{
    [Authorize(Roles = "Developer")]
    public class DeveloperController : Controller
    {
        private readonly IDeveloperService _developerService;

        public DeveloperController(IDeveloperService developerService)
        {
            _developerService = developerService;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserEmail = User.FindFirstValue(ClaimTypes.Email);
            var tasks = await _developerService.GetAssignedTasksAsync(currentUserEmail);
            return View(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int taskId, string status)
        {
            var result = await _developerService.UpdateTaskStatusAsync(taskId, status);
            return Json(new { success = result });
        }
    }
}
