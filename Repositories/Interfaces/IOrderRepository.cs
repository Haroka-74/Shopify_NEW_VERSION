using Shopify.Models;

namespace Shopify.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersAsync();
        Task<Order?> GetOrderAsync(int id);
        Task<Order> AddOrderAsync(Order order);
        Task<Order?> UpdateOrderAsync(int id, Order order);
        Task DeleteOrderAsync(int id);
        Task<List<Order>> GetUserOrders(string userId);
        Task SaveChangesAsync();
    }
}