using Shopify.DTOs.ProductDTOs;
using Shopify.Models;
using Shopify.Repositories.Interfaces;
using Shopify.Services.Interfaces;
using Shopify.Shared;

namespace Shopify.Services.Implementations
{
    public class ProductService(IProductRepository productRepository) : IProductService
    {

        private readonly IProductRepository productRepository = productRepository;

        public async Task<List<ProductDTO>> GetProductsAsync()
        {
            var products = await productRepository.GetProductsAsync();
            var result = products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Image = p.Image,
                Stock = p.Stock,
                CategoryName = p.Category.Name
            }).ToList();
            return result;
        }

        public async Task<Result<ProductDTO>> GetProductAsync(int id)
        {
            var product = await productRepository.GetProductAsync(id);

            if (product is null)
                return Result<ProductDTO>.Failure($"Product with id = {id} is not found", 404);

            return Result<ProductDTO>.Success(new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image,
                Stock = product.Stock,
                CategoryName = product.Category.Name
            }, 200);
        }

        public async Task<Product> AddProductAsync(CreateProductDTO createProductDTO)
        {
            return await productRepository.AddProductAsync(new Product
            {
                Name = createProductDTO.Name,
                Description = createProductDTO.Description,
                Price = createProductDTO.Price,
                Image = createProductDTO.Image,
                Stock = createProductDTO.Stock,
                CategoryId = createProductDTO.CategoryId,
            });
        }

        public async Task<Result<Product>> UpdateProductAsync(int id, CreateProductDTO createProductDTO)
        {
            var result = await productRepository.UpdateProductAsync(id, new Product
            {
                Name = createProductDTO.Name,
                Description = createProductDTO.Description,
                Price = createProductDTO.Price,
                Image = createProductDTO.Image,
                Stock = createProductDTO.Stock,
                CategoryId = createProductDTO.CategoryId,
            });

            if (result is null)
                return Result<Product>.Failure($"Product with id = {id} is not found", 404);

            return Result<Product>.Success(result, 200);
        }

        public async Task<Result<object>> DeleteProductAsync(int id)
        {
            var result = await productRepository.DeleteProductAsync(id);

            if (!result)
                return Result<object>.Failure($"Product with id = {id} is not found", 404);

            return Result<object>.Success(null!, 204);
        }

    }
}