using Microsoft.AspNetCore.Mvc;
using VirtualStore.Accessor.Models;
using VirtualStore.Accessor.Services;

namespace VirtualStore.Accessor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsAccessorController : ControllerBase
    {
        private readonly ILogger<ProductsAccessorController> _logger;
        private readonly ProductsService _productsService;

        public ProductsAccessorController(ILogger<ProductsAccessorController> logger, ProductsService productsService)
        {
            _logger = logger;
            _productsService = productsService;
        }
        [HttpGet("/products")]
        public async Task<ActionResult<List<Products>>> GetAllProductsAsync()
        {
            try
            {
                _logger.LogInformation("GetAllProductsAsync");

                var result = await _productsService.GetAllAsync();

                if (result == null || result.Count == 0)
                {
                    _logger.LogInformation("No products");
                    return NotFound("Products not found");
                }

                _logger.LogInformation("GetAllProductsAsync successfully");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: {ex}", ex.Message);
                return Problem();
            }
        }

        [HttpGet("/product/{id}")]
        public async Task<ActionResult<Products>> GetProductByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("GetProductByIdAsync");

                var result = await _productsService.GetByIdAsync(id);

                if (result == null)
                {
                    _logger.LogInformation("ProductID {id} not found", id);
                    return NotFound("Product not found");
                }

                _logger.LogInformation("ProductID {id} retrieved", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: {ex}", ex.Message);
                return Problem();
            }
        }

        [HttpPost("/product")]
        public async Task<ActionResult<Products>> AddProductAsync(Products product)
        {
            try
            {
                _logger.LogInformation("AddProductAsync");

                var result = await _productsService.AddProductAsync(product);

                if (result == null)
                {
                    _logger.LogInformation("Faild add product to DB");
                    return BadRequest("Faild to add product");
                }

                _logger.LogInformation("successfully added product");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: {ex}", ex.Message);
                return Problem();
            }
        }

        [HttpPut("/product")]
        public async Task<ActionResult> UpdateProductAsync(Products product)
        {
            try
            {
                _logger.LogInformation("UpdateProductAsync");

                await _productsService.UpdateProductAsync(product);

                _logger.LogInformation("successfully updated");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: {ex}", ex.Message);
                return Problem();
            }
        }

        [HttpDelete("/product/{id}")]
        public async Task<ActionResult> DeleteProductAsync(int id)
        {
            try
            {
                _logger.LogInformation("DeleteProductAsync");

                var deletedCount = await _productsService.DeleteProductAsync(id);

                if (deletedCount == 0)
                {
                    _logger.LogInformation("No productID {id}", id);
                    return NotFound("Product not found");
                }

                _logger.LogInformation("Product with currect ID {id} successfully deleted", id);
                return Ok(deletedCount);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception: {ex}", ex.Message);
                return Problem();
            }
        }
    }
}
