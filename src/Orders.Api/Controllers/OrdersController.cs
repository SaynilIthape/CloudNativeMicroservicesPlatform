using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Api.Data;
using Orders.Api.Models.DTOs;
using Orders.Api.Models.Entities;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(OrdersDbContext dbContext): ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var orders=  dbContext.Orders
            .Include(x=>x.Items)
            .AsNoTracking()
            .ToArrayAsync();

        return Ok(await orders);    
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var order = await dbContext.Orders
            .Include(x => x.Items)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (order is null)
            return NotFound();

        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
    {
        if (request.Items.Count == 0)
            return BadRequest("Order must contain at least one item.");

        if (request.Items.Any(x => x.Quantity <= 0 || x.UnitPrice < 0))
            return BadRequest("Invalid quantity or price.");

        var order = new Order
        {
            CustomerId = request.CustomerId,
            Status = OrderStatus.Pending,
            OrderDate = DateTime.UtcNow,
            Items = request.Items.Select(item => new OrderItem
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            }).ToList()
        };

        order.TotalAmount = order.Items.Sum(x => x.Subtotal);

        dbContext.Orders.Add(order);

        await dbContext.SaveChangesAsync();

        return CreatedAtAction(
            nameof(GetOrder),
            new { id = order.Id },
            order);
    }
}