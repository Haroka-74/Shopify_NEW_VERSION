using Microsoft.EntityFrameworkCore;
using Shopify.Data;
using Shopify.Models;
using Shopify.Repositories.Interfaces;

namespace Shopify.Repositories.Implementations
{
    public class CartRepository(ShopifyDbContext context) : ICartRepository
    {

        private readonly ShopifyDbContext context = context;

        public async Task<Cart?> GetUserCartAsync(string userId) => await context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product).FirstOrDefaultAsync(c => c.UserId == userId);

        public async Task<Cart> AddItemToCartAsync(string userId, CartItem cartItem)
        {
            var cart = await GetUserCartAsync(userId);
            cartItem.AddedAt = DateTime.Now;
            if (cart is null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CartItems = [cartItem]
                };
                context.Carts.Add(cart);
            }
            else
            {
                var item = GetCartItem(cart, cartItem.ProductId);
                if (item is null)
                {
                    cart.CartItems.Add(cartItem);
                }
                else
                {
                    item.Quantity += cartItem.Quantity;
                    item.AddedAt = DateTime.Now;
                }
            }            
            await SaveChangesAsync();
            return cart;
        }

        public async Task RemoveItemFromCartAsync(string userId, int productId)
        {
            var cart = await GetUserCartAsync(userId);

            if (cart is null) 
                return;

            var item = GetCartItem(cart, productId);

            if (item is null) 
                return;
            
            context.CartItems.Remove(item);
            await SaveChangesAsync();
        }

        public async Task<Cart?> UpdateItemQuantityAsync(string userId, int productId, int quantity)
        {
            var cart = await GetUserCartAsync(userId);

            if(cart is null) 
                return null;

            var item = GetCartItem(cart, productId);
            
            if (item is null) 
                return cart;

            if (quantity <= 0)
            {
                await RemoveItemFromCartAsync(userId, productId);
            }
            else
            {
                item.Quantity = quantity;
            }

            await SaveChangesAsync();
            return cart;
        }

        public async Task ClearUserCartAsync(string userId)
        {
            var cart = await GetUserCartAsync(userId);
            
            if(cart is null) 
                return;
            
            cart.CartItems.Clear();
            await SaveChangesAsync();
        }

        private static CartItem? GetCartItem(Cart cart, int productId) => cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

    }
}