using Microsoft.EntityFrameworkCore;
using Shopify.Data;
using Shopify.Models;
using Shopify.Repositories.Interfaces;

namespace Shopify.Repositories.Implementations
{
    public class CategoryRepository(ShopifyDbContext context) : ICategoryRepository
    {

        private readonly ShopifyDbContext context = context;

        public async Task<List<Category>> GetCategoriesAsync() => await context.Categories.Include(c => c.Products).ToListAsync();

        public async Task<Category?> GetCategoryAsync(int id) => await context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);

        public async Task<Category> AddCategoryAsync(Category category)
        {
            context.Categories.Add(category);
            await SaveChangesAsync();
            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = await GetCategoryAsync(id);

            if (existingCategory is null) 
                return null;
            
            context.Entry(existingCategory).CurrentValues.SetValues(category);
            await SaveChangesAsync();
            
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryAsync(id);

            if (category is null) 
                return false;

            context.Categories.Remove(category);
            await SaveChangesAsync();

            return true;
        }

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

    }
}