using Shopify.Models;

namespace Shopify.Repositories.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart?> GetUserCartAsync(string userId);
        Task<Cart> AddItemToCartAsync(string userId, CartItem cartItem);
        Task RemoveItemFromCartAsync(string userId, int productId);
        Task<Cart?> UpdateItemQuantityAsync(string userId, int productId, int newQuantity);
        Task ClearUserCartAsync(string userId);
        Task SaveChangesAsync();
    }
}