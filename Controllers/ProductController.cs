using Microsoft.AspNetCore.Mvc;
using Shopify.DTOs.ProductDTOs;
using Shopify.Services.Interfaces;
using Shopify.Shared;

namespace Shopify.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {

        private readonly IProductService productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetProductsAsync() => Ok(await productService.GetProductsAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var result = await productService.GetProductAsync(id);

            if (result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);

            return StatusCode(result.StatusCode, new { error = result.Error });
        }

        [HttpPost]
        public async Task<IActionResult> AddProductAsync(CreateProductDTO createProductDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var product = await productService.AddProductAsync(createProductDTO);

            return CreatedAtAction(nameof(GetProductAsync), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductAsync(int id, CreateProductDTO createProductDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage) });

            var result = await productService.UpdateProductAsync(id, createProductDTO);

            if (result.IsSuccess)
                return StatusCode(result.StatusCode, result.Data);

            return StatusCode(result.StatusCode, new { error = result.Error });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            var result = await productService.DeleteProductAsync(id);

            if (!result.IsSuccess)
                return StatusCode(result.StatusCode, new { error = result.Error });

            return StatusCode(result.StatusCode);
        }

    }
}