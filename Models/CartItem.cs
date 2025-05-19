using System.ComponentModel.DataAnnotations;

namespace Shopify.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "CartId is required.")]
        public int CartId { get; set; }
        [Required(ErrorMessage = "ProductId is required.")]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "AddedAt is required.")]
        public DateTime AddedAt { get; set; } = DateTime.Now;
        public Cart Cart { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}