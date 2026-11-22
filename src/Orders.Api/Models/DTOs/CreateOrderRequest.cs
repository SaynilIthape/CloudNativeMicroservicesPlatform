namespace Orders.Api.Models.DTOs;

public class CreateOrderRequest
{
    public string CustomerId { get; set; } = string.Empty;

    public List<CreateOrderItemRequest> Items { get; set; } = new();
}