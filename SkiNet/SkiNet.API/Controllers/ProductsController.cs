
namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(AppDbContext context) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult> GetProducts()
    {
        return Ok(await context.Products.Where(p => ! p.IsDeleted).ToListAsync());
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetProduct(Guid id)
    {
        Product? product =await context.Products.FirstOrDefaultAsync(p => p.Id == id && ! p.IsDeleted);
        if (product == null) return NotFound();

        return Ok(product);
    }
    [HttpPost]
    public async Task<ActionResult> AddProduct(Product product)
    {
        product.CreatedAt = DateTime.Now;
        product.Id = Guid.NewGuid();
        context.Products.Add(product);
        
        await context.SaveChangesAsync();

        return Created($"api/products/{product.Id}", product);
    }
    [HttpPut]
    public async Task<ActionResult> UpdateProduct(Product product)
    {
        if (!await ProductExists(product.Id))
            return NotFound();

        context.Entry(product).State = EntityState.Modified;

        if (!(await context.SaveChangesAsync() > 0))
            return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        Product? product = await context.Products.FirstOrDefaultAsync(p => p.Id ==id && !p.IsDeleted);
        if (product == null) return NotFound();

        product.IsDeleted = true;
        product.DeletedAt = DateTime.Now;
        context.Entry(product).State = EntityState.Modified;

        if (!(await context.SaveChangesAsync() > 0))
            return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    private async Task<bool> ProductExists(Guid id)
    {
        return await context.Products.AnyAsync(p => p.Id == id && !p.IsDeleted);
    }
}
