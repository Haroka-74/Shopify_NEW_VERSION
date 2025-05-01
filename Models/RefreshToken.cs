using System.ComponentModel.DataAnnotations;

namespace Shopify.Models
{
    public class RefreshToken
    {
        public string Id { get; set; } = null!;
        [Required(ErrorMessage = "Token is required.")]
        public string Token { get; set; } = null!;
        [Required(ErrorMessage = "CreatedAt is required.")]
        public DateTime CreatedAt { get; set; }
        [Required(ErrorMessage = "ExpiresAt is required.")]
        public DateTime ExpiresAt { get; set; }
        [Required(ErrorMessage = "IsRevoked is required.")]
        public bool IsRevoked { get; set; }
        [Required(ErrorMessage = "UserId is required.")]
        public string UserId { get; set; } = null!;
        public ShopifyUser User { get; set; } = null!;
    }
}