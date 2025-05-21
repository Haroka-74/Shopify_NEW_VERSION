using System.ComponentModel.DataAnnotations;

namespace Shopify.DTOs.AuthDTOs
{
    public class RefreshTokenDTO
    {
        [Required(ErrorMessage = "Token is required")]
        public string Token { get; set; } = null!;
    }
}