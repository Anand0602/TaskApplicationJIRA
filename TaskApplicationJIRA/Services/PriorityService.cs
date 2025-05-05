// Services/Implementations/PriorityService.cs
using Microsoft.EntityFrameworkCore;
using TaskApplicationJIRA.Data;
using TaskApplicationJIRA.Models.PriorityModel;
using TaskApplicationJIRA.Services.Interfaces;

namespace TaskApplicationJIRA.Services
{
   

    public class PriorityService : IPriorityService
    {
        private readonly ApplicationDbContext _context;

        public PriorityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Priority>> GetAllAsync()
        {
            return await _context.Priorities.ToListAsync();
        }

        public async Task<Priority?> GetByIdAsync(int id)
        {
            return await _context.Priorities.FindAsync(id);
        }

        public async Task AddAsync(Priority priority)
        {
            _context.Priorities.Add(priority);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Priority priority)
        {
            _context.Priorities.Update(priority);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var priority = await _context.Priorities.FindAsync(id);
            if (priority != null)
            {
                _context.Priorities.Remove(priority);
                await _context.SaveChangesAsync();
            }
        }

        public bool Exists(int id)
        {
            return _context.Priorities.Any(e => e.PriorityId == id);
        }
    }

}
