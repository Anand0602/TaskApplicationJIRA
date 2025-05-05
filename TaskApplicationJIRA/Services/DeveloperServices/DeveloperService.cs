// Services/Developer/DeveloperService.cs
using Microsoft.EntityFrameworkCore;
using TaskApplicationJIRA.Data;
using TaskApplicationJIRA.Models.ViewModels;

namespace TaskApplicationJIRA.Services.DeveloperServices
{
    public class DeveloperService : IDeveloperService
    {
        private readonly ApplicationDbContext _context;

        public DeveloperService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskAssignedDevView>> GetAssignedTasksAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return new List<TaskAssignedDevView>();

            var assignedTasks = await _context.TaskAssignments
                .Where(ta => ta.AssignedToUserId == user.UserId)
                .Include(ta => ta.Task)
                    .ThenInclude(task => task.Category)
                .Include(ta => ta.Task)
                    .ThenInclude(task => task.Priority)
                .ToListAsync();

            return assignedTasks.Select(ta => new TaskAssignedDevView
            {
                TaskId = ta.Task.Id,
                Title = ta.Task.Title,
                Description = ta.Task.Description,
                CategoryName = ta.Task.Category?.Name,
                PriorityLevel = ta.Task.Priority?.Level,
                Status = ta.Task.Status,
                DueDate = ta.Task.DueDate,
                ImageUrl = ta.Task.ImageUrl
            }).ToList();
        }

        public async Task<bool> UpdateTaskStatusAsync(int taskId, string status)
        {
            var taskAssignment = await _context.TaskAssignments
                .Include(ta => ta.Task)
                .FirstOrDefaultAsync(ta => ta.TaskId == taskId);

            if (taskAssignment == null) return false;

            taskAssignment.Task.Status = status;
            taskAssignment.Task.UpdatedOn = DateTime.Now;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
