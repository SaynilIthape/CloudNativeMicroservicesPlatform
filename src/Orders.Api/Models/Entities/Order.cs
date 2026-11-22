namespace Orders.Api.Models.Entities;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string CustomerId { get; set; } = string.Empty;

    public DateTime OrderDate { get; set; } = DateTime.UtcNow;

    public OrderStatus Status { get; set; } = OrderStatus.Pending;

    public decimal TotalAmount { get; set; }

    public List<OrderItem> Items { get; set; } = new();
}