using Microsoft.AspNetCore.Mvc;
using TaskApplicationJIRA.Models.ViewModels;
using TaskApplicationJIRA.Services.Interfaces;

namespace TaskApplicationJIRA.Controllers.TaskControllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // Create Task - GET
        public async Task<IActionResult> Create()
        {
            var model = await _taskService.GetCreateTaskViewModelAsync();
            return View(model);
        }

        // Create Task - POST
        [HttpPost]
        public async Task<IActionResult> Create(TaskItemViewModel model, IFormFile image)
        {
            await _taskService.CreateTaskAsync(model, image);
            TempData["SuccessMessage"] = "Task created successfully!";
            return RedirectToAction("Index");
        }

        // Edit Task - GET
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _taskService.GetEditTaskViewModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }

        // Edit Task - POST
        [HttpPost]
        public async Task<IActionResult> Edit(int id, TaskItemViewModel model, IFormFile image)
        {
            var success = await _taskService.EditTaskAsync(id, model, image);
            if (!success) return NotFound();

            TempData["SuccessMessage"] = "Task updated successfully!";
            return RedirectToAction("Index");
        }

        // Delete Task - GET
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _taskService.GetTaskForDeleteAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // Delete Task - POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            TempData["SuccessMessage"] = "Task deleted successfully!";
            return RedirectToAction("Index");
        }

        // Index - Display All Tasks
        public async Task<IActionResult> Index()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return View(tasks);
        }
    }
}
