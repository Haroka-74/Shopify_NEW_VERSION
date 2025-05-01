using System.ComponentModel.DataAnnotations;

namespace Shopify.Models
{
    public class Cart
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; } = null!;
        [Required(ErrorMessage = "ProductId is required.")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "AddedAt is required.")]
        public DateTime AddedAt { get; set; } = DateTime.Now;
        public ShopifyUser User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}