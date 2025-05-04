using Shopify.DTOs.CategoryDTOs;
using Shopify.Models;
using Shopify.Repositories.Interfaces;
using Shopify.Services.Interfaces;

namespace Shopify.Services.Implementations
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {

        private readonly ICategoryRepository categoryRepository = categoryRepository;

        public async Task<List<CategoryDTO>> GetCategoriesAsync()
        {
            var categories = await categoryRepository.GetCategoriesAsync();
            var result = categories.Select(c => new CategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ProductsNames = [.. c.Products.Select(p => p.Name)]
            }).ToList();
            return result;
        }

        public async Task<CategoryDTO?> GetCategoryAsync(int id)
        {
            var category = await categoryRepository.GetCategoryAsync(id);
            if (category is null) return null;
            var result = new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ProductsNames = [.. category.Products.Select(p => p.Name)]
            };
            return result;
        }

        public async Task<Category> AddCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            return await categoryRepository.AddCategoryAsync(new Category
            {
                Name = createCategoryDTO.Name,
                Description = createCategoryDTO.Description,
            });
        }

        public async Task<Category?> UpdateCategoryAsync(int id, CreateCategoryDTO createCategoryDTO)
        {
            return await categoryRepository.UpdateCategoryAsync(id, new Category
            {
                Name = createCategoryDTO.Name,
                Description = createCategoryDTO.Description,
            });
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await categoryRepository.DeleteCategoryAsync(id);
        }

    }
}