using System.ComponentModel.DataAnnotations;

namespace Shopify.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; } = null!;
        [Required(ErrorMessage = "TotalPrice is required.")]
        public decimal TotalPrice { get; set; }
        [Required(ErrorMessage = "CreatedAt is required.")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public ShopifyUser User { get; set; } = null!;
        public ICollection<OrderDetail> OrderDetails { get; set; } = [];
    }
}