﻿using Microsoft.EntityFrameworkCore;
using Shopify.Data;
using Shopify.Models;
using Shopify.Repositories.Interfaces;

namespace Shopify.Repositories.Implementations
{
    public class ProductRepository(ShopifyDbContext context) : IProductRepository
    {

        private readonly ShopifyDbContext context = context;

        public async Task<List<Product>> GetProductsAsync() => await context.Products.Include(p => p.Category).ToListAsync();

        public async Task<Product?> GetProductAsync(int id) => await context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Product> AddProductAsync(Product product)
        {
            context.Products.Add(product);
            await SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateProductAsync(int id, Product product)
        {
            var existingProduct = await GetProductAsync(id);

            if (existingProduct is null) 
                return null;

            context.Entry(existingProduct).CurrentValues.SetValues(product);
            await SaveChangesAsync();

            return existingProduct;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await GetProductAsync(id);

            if (product is null) 
                return false;

            context.Products.Remove(product);
            await SaveChangesAsync();
            
            return true;
        }

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

    }
}