using Shopify.DTOs.CategoryDTOs;
using Shopify.Models;

namespace Shopify.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDTO>> GetCategoriesAsync();
        Task<CategoryDTO?> GetCategoryAsync(int id);
        Task<Category> AddCategoryAsync(CreateCategoryDTO createCategoryDTO);
        Task<Category?> UpdateCategoryAsync(int id, CreateCategoryDTO createCategoryDTO);
        Task DeleteCategoryAsync(int id);
    }
}