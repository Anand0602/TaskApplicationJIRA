using System.ComponentModel.DataAnnotations;
using TaskApplicationJIRA.Models.CategoryModel;
using TaskApplicationJIRA.Models.PriorityModel;

namespace TaskApplicationJIRA.Models.ViewModels
{
    public class TaskItemViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Task Title is required.")]
        [StringLength(100, ErrorMessage = "Task Title can't be longer than 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Task Description is required.")]
        [StringLength(500, ErrorMessage = "Task Description can't be longer than 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public int CategoryId { get; set; }

        public string Status { get; set; }
        public int PriorityId { get; set; }
        public Priority Priority { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? DueDate { get; set; }

        [Url(ErrorMessage = "Invalid Image URL.")]
        public string ImageUrl { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Priority> Priorities { get; set; }
    }
}
