using TaskApplicationJIRA.Models.UserModel;

namespace TaskApplicationJIRA.ViewModels
{
    public class ScrumMasterTaskViewModel
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string PriorityLevel { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
        public string ImageUrl { get; set; }
        public int? AssignedToUserId { get; set; }
        public string AssignedToUserName { get; set; }
    }

    public class ScrumMasterViewModel
    {
        public List<ScrumMasterTaskViewModel> Tasks { get; set; }
        public List<User> Developers { get; set; }
    }
}
