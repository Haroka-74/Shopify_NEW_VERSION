using Shopify.DTOs.ProductDTOs;
using Shopify.Models;
using Shopify.Shared;

namespace Shopify.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetProductsAsync();
        Task<Result<ProductDTO>> GetProductAsync(int id);
        Task<Product> AddProductAsync(CreateProductDTO createProductDTO);
        Task<Result<Product>> UpdateProductAsync(int id, CreateProductDTO createProductDTO);
        Task<Result<object>> DeleteProductAsync(int id);
    }
}