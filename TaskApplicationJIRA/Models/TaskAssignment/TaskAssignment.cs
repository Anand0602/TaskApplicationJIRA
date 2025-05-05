using TaskApplicationJIRA.Models.TaskModel;
using TaskApplicationJIRA.Models.UserModel;

namespace TaskApplicationJIRA.Models.TaskAssignment
{
    public class TaskAssignment
    {
        public int TaskAssignmentId { get; set; }

        public int TaskId { get; set; }
        public TaskItem Task { get; set; }

        public int AssignedToUserId { get; set; }
        public User AssignedTo { get; set; }

        // You can also add an AssignedDate if you want

        // Audit fields
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }

}
