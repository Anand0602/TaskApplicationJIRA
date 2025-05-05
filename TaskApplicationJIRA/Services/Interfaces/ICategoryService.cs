// Services/Interfaces/ICategoryService.cs
using TaskApplicationJIRA.Models.CategoryModel;

public interface ICategoryService
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(int id);
    bool CategoryExists(int id);
}
