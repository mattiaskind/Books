using Books.Interfaces;
using Books.Models;
using Microsoft.EntityFrameworkCore;

namespace Books.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public IQueryable<Category> Categories => _context.Categories.AsQueryable();

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await Categories.ToListAsync();
        }

        public async Task<Category?> GetCategoryByNameAsync(string categoryName)
        {
            return await _context.Categories.Where(x => x.CategoryName == categoryName).SingleOrDefaultAsync();
        }

        public async Task<Category> CreateCategoryAsync(Category newCategory)
        {
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();

            return newCategory;
        }

        public bool CategoryExistsAsync(string categoryName)
        {
            return _context.Categories.Any(c => c.CategoryName == categoryName);
        }

    }
}
