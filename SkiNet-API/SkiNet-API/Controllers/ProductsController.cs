using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiNet_API.Entities;

namespace SkiNet_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase 
{
    private readonly StoreContext context;

    public ProductsController(StoreContext context)
    {
        this.context = context;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
    {
        return await context.Products.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        Product product = await context.Products.FindAsync(id);
        if (product is null)
            return NotFound();

        return Ok(product);
    }
    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct([FromForm] Product product)
    {
        if (product == null)
            return NotFound();

        context.Products.Add(product);
        await context.SaveChangesAsync();

        return Ok();
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        bool isExisting = await ProductExists(id);
        if (id != product.Id || !isExisting)
            return NotFound();

        context.Entry(product).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        Product product =await context.Products.FindAsync(id);
        if (product is null)
            return NotFound();

        context.Products.Remove(product);
        await context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> ProductExists(int id)
    {
        return await context.Products.AnyAsync(p => p.Id == id);
    }
}
