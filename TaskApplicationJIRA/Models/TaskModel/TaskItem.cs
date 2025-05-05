using System.ComponentModel.DataAnnotations;
using TaskApplicationJIRA.Models.CategoryModel;
using TaskApplicationJIRA.Models.PriorityModel;

namespace TaskApplicationJIRA.Models.TaskModel
{
    public class TaskItem
    {
        public int Id { get; set; }

        // Task Title (Required with max length validation)
        [Required(ErrorMessage = "Task Title is required.")]
        [StringLength(100, ErrorMessage = "Task Title can't be longer than 100 characters.")]
        public string Title { get; set; }

        // Task Description (Required with max length validation)
        [Required(ErrorMessage = "Task Description is required.")]
        [StringLength(500, ErrorMessage = "Task Description can't be longer than 500 characters.")]
        public string Description { get; set; }

        // Category (Required)
        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Priority (Required)
        [Required(ErrorMessage = "Priority is required.")]
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }

        // Status (Optional)
        [StringLength(50, ErrorMessage = "Status can't be longer than 50 characters.")]
        public string Status { get; set; } // Example: "To-Do", "In Progress", "Done"

        

        // Due Date (Optional)
        public DateTime? DueDate { get; set; }

        // Audit fields
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }

        // Image URL (Optional)
        [Url(ErrorMessage = "Invalid Image URL.")]
        public string ImageUrl { get; set; }
    }

}
