using Microsoft.EntityFrameworkCore;
using VirtualStore.Accessor.Models;

namespace VirtualStore.Accessor.Services
{
    public class ProductsService
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ProductsService> _logger;

        public ProductsService(AppDbContext db, ILogger<ProductsService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<List<Products>> GetAllAsync()
        {
            try
            {
                return await _db.Products.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                throw;
            }
        }

        public async Task<Products?> GetByIdAsync(int productId)
        {
            try
            {
                return await _db.Products.FindAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error");
                throw;
            }
        }

        public async Task<Products> AddProductAsync(Products product)
        {
            try
            {
                await _db.Products.AddAsync(product);
                await _db.SaveChangesAsync();

                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error");
                throw;
            }
        }

        public async Task UpdateProductAsync(Products product)
        {
            try
            {
                _db.Products.Update(product);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error{product.ProductID}");
                throw;
            }
        }

        public async Task<int> DeleteProductAsync(int productId)
        {
            try
            {
                var product = await _db.Products.FindAsync(productId);
                if (product == null)
                    return 0;

                _db.Products.Remove(product);
                return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error{productId}");
                throw;
            }
        }
    }

}
