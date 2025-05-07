using Microsoft.EntityFrameworkCore;
using TaskApplicationJIRA.Models.CategoryModel;
using TaskApplicationJIRA.Models.PriorityModel;
using TaskApplicationJIRA.Models.UserModel;
using TaskApplicationJIRA.Models.TaskModel;
using TaskApplicationJIRA.Models.TaskAssignment;

namespace TaskApplicationJIRA.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets for  tables
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }

        
    }
}
