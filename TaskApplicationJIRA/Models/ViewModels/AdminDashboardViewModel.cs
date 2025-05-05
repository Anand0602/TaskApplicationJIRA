using TaskApplicationJIRA.Models.CategoryModel;
using TaskApplicationJIRA.Models.PriorityModel;
using TaskApplicationJIRA.Models.TaskModel;
using TaskApplicationJIRA.Models.UserModel;

namespace TaskApplicationJIRA.Models.ViewModels
{
    public class AdminDashboardViewModel
    {
        public List<User> Users { get; set; }
        public List<TaskItem> Tasks { get; set; }
        public List<Category> Categories { get; set; }
        public List<Priority> Priorities { get; set; }
    }
}
