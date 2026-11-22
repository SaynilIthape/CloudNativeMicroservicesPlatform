using Inventory.Api.Models;
using Microsoft.Azure.Cosmos;

namespace Inventory.Api.Services;


public class ProductService(Container container) : IProductService
{
    public async Task<Product> CreateAsync(Product product)
    {
        product.Id = product.ProductId;
        var response =  await container.CreateItemAsync(product, new PartitionKey(product.Category));   

        return response.Resource;
    }

    public async Task<List<Product>> GetAllAsync()
    {
        var query =  container.GetItemQueryIterator<Product>(new QueryDefinition("SELECT * FROM c"));
        var products = new List<Product>();

        while (query.HasMoreResults)
        {
            var response = await query.ReadNextAsync();
            products.AddRange(response);
        }
        return products;
    }

    public async Task<Product?> GetByIdAsync(string productId, string category)
    {
        try
        {
            var response =  await container.ReadItemAsync<Product>(productId, new PartitionKey(category));
            return response.Resource;
        }
        catch(CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}