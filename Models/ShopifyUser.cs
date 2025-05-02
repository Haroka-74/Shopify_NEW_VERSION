using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopify.Models
{
    public class ShopifyUser : IdentityUser
    {
        [Required(ErrorMessage = "Balance is required.")]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Balance cannot be negative.")]
        public decimal Balance { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
        public ICollection<Cart> Carts { get; set; } = [];
        public ICollection<Order> Orders { get; set; } = [];
    }
}