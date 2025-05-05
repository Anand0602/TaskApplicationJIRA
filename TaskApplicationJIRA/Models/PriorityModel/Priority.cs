namespace TaskApplicationJIRA.Models.PriorityModel
{
    public class Priority
    {
        public int PriorityId { get; set; }
        public string Level { get; set; } // High, Low

        // Audit fields
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }

}
