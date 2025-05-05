// Services/ScrumMaster/ScrumMasterService.cs
using Microsoft.EntityFrameworkCore;
using TaskApplicationJIRA.Data;
using TaskApplicationJIRA.Models.TaskAssignment;
using TaskApplicationJIRA.ViewModels;

namespace TaskApplicationJIRA.Services.ScrumMaster
{
    public class ScrumMasterService : IScrumMasterService
    {
        private readonly ApplicationDbContext _context;

        public ScrumMasterService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ScrumMasterViewModel> GetDashboardAsync()
        {
            var tasks = await _context.Tasks
                .Include(t => t.Category)
                .Include(t => t.Priority)
                .ToListAsync();

            var developers = await _context.Users
                .Where(u => u.Role == "Developer")
                .ToListAsync();

            var assignments = await _context.TaskAssignments
                .Where(a => a.DeletedOn == null)
                .ToListAsync();

            var taskViewModels = tasks.Select(task =>
            {
                var assignment = assignments.FirstOrDefault(a => a.TaskId == task.Id);
                return new ScrumMasterTaskViewModel
                {
                    TaskId = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    CategoryName = task.Category?.Name,
                    PriorityLevel = task.Priority?.Level,
                    Status = task.Status,
                    DueDate = task.DueDate,
                    ImageUrl = task.ImageUrl,
                    AssignedToUserId = assignment?.AssignedToUserId
                };
            }).ToList();

            return new ScrumMasterViewModel
            {
                Tasks = taskViewModels,
                Developers = developers
            };
        }

        public async Task AssignTaskAsync(int taskId, int developerId)
        {
            var existingAssignment = await _context.TaskAssignments
                .FirstOrDefaultAsync(t => t.TaskId == taskId && t.DeletedOn == null);

            if (existingAssignment == null)
            {
                var assignment = new TaskAssignment
                {
                    TaskId = taskId,
                    AssignedToUserId = developerId,
                    CreatedBy = 1, // Replace with actual userId from session/context
                    CreatedOn = DateTime.Now
                };
                _context.TaskAssignments.Add(assignment);
            }
            else
            {
                existingAssignment.AssignedToUserId = developerId;
                existingAssignment.UpdatedBy = 1; // Replace with actual userId
                existingAssignment.UpdatedOn = DateTime.Now;
            }

            await _context.SaveChangesAsync();
        }
    }
}
