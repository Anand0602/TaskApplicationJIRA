namespace TaskApplicationJIRA.Models.ViewModels
{
    public class TaskAssignedDevView
    {
        public int TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string PriorityLevel { get; set; }
        public string Status { get; set; }
        public DateTime? DueDate { get; set; }
        public string ImageUrl { get; set; }
    }
}
