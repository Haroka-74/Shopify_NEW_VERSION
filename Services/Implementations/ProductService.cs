using Shopify.DTOs.ProductDTOs;
using Shopify.Models;
using Shopify.Repositories.Interfaces;
using Shopify.Services.Interfaces;

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

        public async Task<ProductDTO?> GetProductAsync(int id)
        {
            var product = await productRepository.GetProductAsync(id);
            if (product is null) return null;
            var result = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image,
                Stock = product.Stock,
                CategoryName = product.Category.Name
            };
            return result;
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

        public async Task<Product?> UpdateProductAsync(int id, CreateProductDTO createProductDTO)
        {
            return await productRepository.UpdateProductAsync(id, new Product
            {
                Name = createProductDTO.Name,
                Description = createProductDTO.Description,
                Price = createProductDTO.Price,
                Image = createProductDTO.Image,
                Stock = createProductDTO.Stock,
                CategoryId = createProductDTO.CategoryId,
            });
        }

        public async Task DeleteProductAsync(int id)
        {
            await productRepository.DeleteProductAsync(id);
        }

    }
}