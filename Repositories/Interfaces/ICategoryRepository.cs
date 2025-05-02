using Shopify.Models;

namespace Shopify.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category?> GetCategoryAsync(int id);
        Task<Category> AddCategoryAsync(Category category);
        Task<Category?> UpdateCategoryAsync(int id, Category category);
        Task DeleteCategoryAsync(int id);
        Task SaveChangesAsync();
    }
}