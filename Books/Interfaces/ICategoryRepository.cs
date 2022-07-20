using Books.Models;

namespace Books.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync(Category newCategory);
        Task<Category?> GetCategoryByNameAsync(string categoryName);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        bool CategoryExistsAsync(string categoryName);
    }
}
