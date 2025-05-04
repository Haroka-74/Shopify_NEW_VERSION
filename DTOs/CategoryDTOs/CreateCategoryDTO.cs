using System.ComponentModel.DataAnnotations;

namespace Shopify.DTOs.CategoryDTOs
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Name length must be between 5 and 50 characters.")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; } = null!;
    }
}