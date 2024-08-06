using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.DAL.Repositories.Categories
{
    public class CategoriesRepository : ICategoriesReposiory
    {
        private readonly AppDBContext _context;
        public CategoriesRepository(AppDBContext context)
        {
            _context = context;
        }
        //ADD
        public async Task<Category> AddCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        //DELETE
        public async Task DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category != null)
            {
                _context.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        //GET
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = await _context.Categories.Include(c => c.Products).ToListAsync();
            return categories;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }

        //UPDATE
        public async Task<Category?> UpdateCategoryAsync(int id, Category category)
        {
            var foundCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (foundCategory != null) { 
                foundCategory.Name = category.Name;
                await _context.SaveChangesAsync();
            }

            return foundCategory;
        }

        public async Task<bool> IsCategoryExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
