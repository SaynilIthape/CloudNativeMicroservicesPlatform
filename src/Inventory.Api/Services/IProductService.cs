
using Inventory.Api.Models;

namespace Inventory.Api.Services;

public interface IProductService
{
    Task<Product> CreateAsync(Product product);

    Task<Product?> GetByIdAsync(string productId, string category);

    Task<List<Product>> GetAllAsync();
}