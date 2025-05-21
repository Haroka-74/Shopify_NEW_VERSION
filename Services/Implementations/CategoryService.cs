using Shopify.DTOs.CategoryDTOs;
using Shopify.Models;
using Shopify.Repositories.Interfaces;
using Shopify.Services.Interfaces;
using Shopify.Shared;

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

        public async Task<Result<CategoryDTO>> GetCategoryAsync(int id)
        {
            var category = await categoryRepository.GetCategoryAsync(id);
            
            if (category is null)
                return Result<CategoryDTO>.Failure($"Category with id = {id} is not found", 404);
                        
            return Result<CategoryDTO>.Success(new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ProductsNames = [.. category.Products.Select(p => p.Name)]
            }, 200);
        }

        public async Task<Category> AddCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            return await categoryRepository.AddCategoryAsync(new Category
            {
                Name = createCategoryDTO.Name,
                Description = createCategoryDTO.Description,
            });
        }

        public async Task<Result<Category>> UpdateCategoryAsync(int id, CreateCategoryDTO createCategoryDTO)
        {
            var result = await categoryRepository.UpdateCategoryAsync(id, new Category
            {
                Name = createCategoryDTO.Name,
                Description = createCategoryDTO.Description,
            });

            if (result is null) 
                return Result<Category>.Failure($"Category with id = {id} is not found", 404);

            return Result<Category>.Success(result, 200);
        }

        public async Task<Result<object>> DeleteCategoryAsync(int id)
        {
            var result = await categoryRepository.DeleteCategoryAsync(id);

            if(!result)
                return Result<object>.Failure($"Category with id = {id} is not found", 404);

            return Result<object>.Success(null!, 204);
        }

    }
}