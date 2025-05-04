using Shopify.DTOs.ProductDTOs;
using Shopify.Models;

namespace Shopify.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProductsAsync();
        Task<ProductDTO?> GetProductAsync(int id);
        Task<Product> AddProductAsync(CreateProductDTO createProductDTO);
        Task<Product?> UpdateProductAsync(int id, CreateProductDTO createProductDTO);
        Task DeleteProductAsync(int id);
    }
}