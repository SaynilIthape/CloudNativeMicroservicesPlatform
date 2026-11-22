
using Inventory.Api.Models;
using Inventory.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IProductService productService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        var created = await productService.CreateAsync(product);

        return CreatedAtAction(
            nameof(GetById),
            new
            {
                productId = created.ProductId,
                category = created.Category
            },
            created);
    }

    [HttpGet("{productId}/{category}")]
    public async Task<IActionResult> GetById(
        string productId, string category)
    {
        var product = await productService.GetByIdAsync(
            productId, category);

        return product is null ? NotFound() : Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await productService.GetAllAsync();

        return Ok(products);
    }
}
