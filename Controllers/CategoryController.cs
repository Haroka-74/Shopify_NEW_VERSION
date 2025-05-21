using Microsoft.AspNetCore.Mvc;
using Shopify.DTOs.CategoryDTOs;
using Shopify.Services.Interfaces;

namespace Shopify.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {

        private readonly ICategoryService categoryService = categoryService;

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAsync() => Ok(await categoryService.GetCategoriesAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryAsync(int id)
        {
            var result = await categoryService.GetCategoryAsync(id);

            if (result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);

            return StatusCode(result.StatusCode, new { error = result.Error });
        }

        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync(CreateCategoryDTO createCategoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var category = await categoryService.AddCategoryAsync(createCategoryDTO);

            return CreatedAtAction(nameof(GetCategoryAsync), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync(int id, CreateCategoryDTO createCategoryDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var result = await categoryService.UpdateCategoryAsync(id, createCategoryDTO);

            if (result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);

            return StatusCode(result.StatusCode, new { error = result.Error });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var result = await categoryService.DeleteCategoryAsync(id);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, new { error = result.Error });

            return StatusCode(result.StatusCode);
        }

    }
}