using Backend.Models;

namespace Backend.DAL.Repositories.Categories
{
    public interface ICategoriesReposiory
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
        Task<Category> AddCategoryAsync(Category category);
        Task<Category?> UpdateCategoryAsync(int id, Category category);
        Task DeleteCategoryAsync(int id);
        Task<bool> IsCategoryExistsAsync(int id);
    }
}
