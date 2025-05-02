using Microsoft.EntityFrameworkCore;
using Shopify.Data;
using Shopify.Models;
using Shopify.Repositories.Interfaces;

namespace Shopify.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {

        private readonly ShopifyDbContext context;

        public OrderRepository(ShopifyDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Order>> GetOrdersAsync() => await context.Orders.Include(o => o.User).Include(o => o.OrderDetails).ToListAsync();

        public async Task<Order?> GetOrderAsync(int id) => await context.Orders.Include(o => o.User).Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == id);

        public async Task<Order> AddOrderAsync(Order order)
        {
            context.Orders.Add(order);
            await SaveChangesAsync();
            return order;
        }

        public async Task<Order?> UpdateOrderAsync(int id, Order order)
        {
            var existingOrder = await GetOrderAsync(id);
            if (existingOrder is null) return null;
            context.Entry(existingOrder).CurrentValues.SetValues(order);
            await SaveChangesAsync();
            return existingOrder;
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await GetOrderAsync(id);
            if (order is null) return;
            context.Orders.Remove(order);
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

    }
}