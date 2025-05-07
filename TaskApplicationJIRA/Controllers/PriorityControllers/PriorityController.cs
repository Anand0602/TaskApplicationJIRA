using Microsoft.AspNetCore.Mvc;
using TaskApplicationJIRA.Models.PriorityModel;
using TaskApplicationJIRA.Services.Interfaces;

namespace TaskApplicationJIRA.Controllers.PriorityControllers
{
    public class PriorityController : Controller
    {
        private readonly IPriorityService _priorityService;

        public PriorityController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        public async Task<IActionResult> Index()
        {
            var priorities = await _priorityService.GetAllAsync();
            return View(priorities);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Priority priority)
        {
            if (ModelState.IsValid)
            {
                await _priorityService.AddAsync(priority);
                TempData["SuccessMessage"] = "Priority created successfully!";
                return RedirectToAction("Index", "Admin");
            }
            return View(priority);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var priority = await _priorityService.GetByIdAsync(id.Value);
            if (priority == null) return NotFound();

            return View(priority);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Priority priority)
        {
            if (id != priority.PriorityId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _priorityService.UpdateAsync(priority);
                    TempData["SuccessMessage"] = "Priority updated successfully!";
                }
                catch
                {
                    if (!_priorityService.Exists(priority.PriorityId)) return NotFound();
                    throw;
                }
                return RedirectToAction("Index", "Admin");
            }
            return View(priority);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var priority = await _priorityService.GetByIdAsync(id.Value);
            if (priority == null) return NotFound();

            return View(priority);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _priorityService.DeleteAsync(id);
            TempData["SuccessMessage"] = "Priority deleted successfully!";
            return RedirectToAction("Index", "Admin");
        }
    }
}
