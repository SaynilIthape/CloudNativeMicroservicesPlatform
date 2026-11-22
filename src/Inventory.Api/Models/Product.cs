
using Newtonsoft.Json;

namespace Inventory.Api.Models;

public class Product
{
    [JsonProperty("id")]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [JsonProperty("productId")]
    public string ProductId { get; set; } = string.Empty;

    [JsonProperty("productName")]
    public string ProductName { get; set; } = string.Empty;

    // Cosmos DB partition key
    [JsonProperty("category")]
    public string Category { get; set; } = string.Empty;

    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("stockQuantity")]
    public int StockQuantity { get; set; }

    [JsonProperty("isActive")]
    public bool IsActive { get; set; } = true;

    [JsonProperty("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
