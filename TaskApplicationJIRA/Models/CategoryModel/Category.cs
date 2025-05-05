namespace TaskApplicationJIRA.Models.CategoryModel
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        // Audit fields
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
