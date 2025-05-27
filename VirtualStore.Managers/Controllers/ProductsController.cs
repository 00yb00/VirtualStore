using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using VirtualStore.Managers.Models;

namespace VirtualStore.Managers.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly DaprClient _daprClient;

        public ProductsController(ILogger<ProductsController> logger, DaprClient daprClient)
        {
            _logger = logger;
            _daprClient = daprClient;
        }

        [HttpGet("/products")]
        public async Task<ActionResult<List<Products>>> GetAllProductsAsync()
        {
            try
            {
                var result = await _daprClient.InvokeMethodAsync<List<Products>>(HttpMethod.Get, "accessor-api", "/products");

                if (result is null)
                {
                    return Ok(new List<Products>());
                }
                return Ok(result);
            }
            catch (InvocationException ie) when (ie.InnerException is HttpRequestException { StatusCode: System.Net.HttpStatusCode.NotFound })
            {
                _logger.LogInformation("No products found");
                return Ok(new List<Products>());
            }
            catch (Exception ex)
            {
                _logger.LogError("ex: {ex}", ex.Message);
                return Problem("Failed to get products");
            }
        }

        [HttpGet("/product")]
        public async Task<ActionResult<Products?>> GetProductByIdAsync([FromQuery(Name = "id")] int id)
        {
            try
            {
                var result = await _daprClient.InvokeMethodAsync<Products?>(HttpMethod.Get, "accessor-api", $"/product/{id}");

                if (result is null)
                {
                    return NotFound("productId not found");
                }
                else
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }
        [HttpPost("/product-sync")]
        public async Task<ActionResult<Products>> AddProduct(Products product)
        {
            try
            {
                var result = await _daprClient.InvokeMethodAsync<Products, Products>(HttpMethod.Post, "accessor-api", "/product", product);

                if (result is null)
                {
                    return Problem("Failed to add product");
                }
                return Ok(result);
            }
            catch (InvocationException ie) when (ie.InnerException is HttpRequestException)
            {
                _logger.LogInformation(" ex: {ie}", ie.Message);
                return StatusCode((int)ie.Response.StatusCode);
            }
            catch (Exception ex)
            {
                return Problem("Failed to add product");
            }
        }
        [HttpDelete("/product")]
        public async Task<ActionResult<string>> DeleteProductByIdAsync([FromQuery(Name = "id")] int id)
        {
            try
            {
                var result = await _daprClient.InvokeMethodAsync<long>(HttpMethod.Delete, "accessor-api", $"/product/{id}");

                return Ok($"Deleted {result} product(s) with id: {id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Problem(ex.Message);
            }
        }


    }
}
