﻿using Microsoft.EntityFrameworkCore;
using Shopify.Data;
using Shopify.Models;
using Shopify.Repositories.Interfaces;

namespace Shopify.Repositories.Implementations
{
    public class OrderRepository(ShopifyDbContext context) : IOrderRepository
    {

        private readonly ShopifyDbContext context = context;

        public async Task<List<Order>> GetOrdersAsync() => await context.Orders.Include(o => o.User).Include(o => o.OrderDetails).ThenInclude(od => od.Product).ToListAsync();

        public async Task<Order?> GetOrderAsync(int id) => await context.Orders.Include(o => o.User).Include(o => o.OrderDetails).ThenInclude(od => od.Product).FirstOrDefaultAsync(o => o.Id == id);

        public async Task<Order> AddOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await SaveChangesAsync();
            return order;
        }

        public async Task<Order?> UpdateOrderAsync(int id, Order order)
        {
            var existingOrder = await GetOrderAsync(id);

            if (existingOrder is null) 
                return null;

            context.Entry(existingOrder).CurrentValues.SetValues(order);
            await SaveChangesAsync();
            
            return existingOrder;
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await GetOrderAsync(id);
            
            if (order is null) 
                return;
            
            context.Orders.Remove(order);
            await SaveChangesAsync();
        }

        public async Task<List<Order>> GetUserOrdersAsync(string userId)
        {
            return await context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
        }

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

    }
}