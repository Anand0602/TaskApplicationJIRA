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

        // DbSets for your tables
        public DbSet<User> Users { get; set; }
        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Priority> Priorities { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }

        // You can override OnModelCreating if needed

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // Explicitly define the primary key for Task
        //    modelBuilder.Entity<Task>()
        //                .HasKey(t => t.Id);
        //}

    }
}
