using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using TaskApplicationJIRA.Data;
using TaskApplicationJIRA.Models.TaskModel;
using TaskApplicationJIRA.Models.ViewModels;
using TaskApplicationJIRA.Services.Interfaces;

namespace TaskApplicationJIRA.Services
{
    

    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TaskService(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<TaskItemViewModel> GetCreateTaskViewModelAsync()
        {
            return new TaskItemViewModel
            {
                Categories = await _context.Categories.ToListAsync(),
                Priorities = await _context.Priorities.ToListAsync()
            };
        }

        public async Task CreateTaskAsync(TaskItemViewModel model, IFormFile image)
        {
            if (image != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                Directory.CreateDirectory(uploadsFolder);
                string uniqueFileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);
                model.ImageUrl = "/images/" + uniqueFileName;
            }

            var task = new TaskItem
            {
                Title = model.Title,
                Description = model.Description,
                CategoryId = model.CategoryId,
                PriorityId = model.PriorityId,
                ImageUrl = model.ImageUrl,
                Status = "To-Do",
                CreatedOn = DateTime.Now
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }

        public async Task<TaskItemViewModel?> GetEditTaskViewModelAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return null;

            return new TaskItemViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                CategoryId = task.CategoryId,
                PriorityId = task.PriorityId,
                Status = task.Status,
                DueDate = task.DueDate,
                ImageUrl = task.ImageUrl,
                Categories = await _context.Categories.ToListAsync(),
                Priorities = await _context.Priorities.ToListAsync()
            };
        }

        public async Task<bool> EditTaskAsync(int id, TaskItemViewModel model, IFormFile image)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            task.Title = model.Title;
            task.Description = model.Description;
            task.CategoryId = model.CategoryId;
            task.PriorityId = model.PriorityId;
            task.Status = model.Status;
            task.DueDate = model.DueDate;
            task.UpdatedOn = DateTime.Now;
            task.UpdatedBy = 1; // mock user ID

            if (image != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid() + Path.GetExtension(image.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var stream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(stream);
                task.ImageUrl = "/images/" + uniqueFileName;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TaskItem?> GetTaskForDeleteAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                task.DeletedOn = DateTime.Now;
                task.DeletedBy = 1; // mock user ID
                _context.Tasks.Remove(task); // or soft delete
                await _context.SaveChangesAsync();
            }
        }
    }

}
