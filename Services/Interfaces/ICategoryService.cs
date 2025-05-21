using Shopify.DTOs.CategoryDTOs;
using Shopify.Models;
using Shopify.Shared;

namespace Shopify.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetCategoriesAsync();
        Task<Result<CategoryDTO>> GetCategoryAsync(int id);
        Task<Category> AddCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task<Result<Category>> UpdateCategoryAsync(int id, CreateCategoryDTO createCategoryDTO);
        Task<Result<object>> DeleteCategoryAsync(int id);
    }
}