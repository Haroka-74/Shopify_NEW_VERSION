using System.ComponentModel.DataAnnotations;

namespace Shopify.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; } = null!;
        [Required(ErrorMessage = "CreatedAt is required.")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ShopifyUser User { get; set; } = null!;
        public ICollection<CartItem> CartItems { get; set; } = [];
    }
}