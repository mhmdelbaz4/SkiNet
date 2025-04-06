using Core.Enums;
using Core.Specifications;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IGenericRepository<Product> repo) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type,ProductSortEnum? sortby ,SortingOptionsEnum? sortOption)
    {
        ProductSpecification spec;
        if(sortby != null)
            spec = new ProductSpecification(brand, type,sortby.Value, sortOption == null ? SortingOptionsEnum.Asc: sortOption.Value);
        else
            spec = new ProductSpecification(brand, type);
        return Ok(await repo.ListSpecAsync(spec));
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult> GetProduct(Guid id)
    {
        Product? product =await repo.GetByIdAsync(id);
        if (product == null) return NotFound();

        return Ok(product);
    }
    [HttpPost]
    public async Task<ActionResult> AddProduct(Product product)
    {
        repo.Add(product);
        if(! await repo.SaveChangesAsync())
            return StatusCode(StatusCodes.Status500InternalServerError);

        return CreatedAtAction(nameof(GetProduct), new { id = product.Id}, product);
    }
    [HttpPut]
    public async Task<ActionResult> UpdateProduct(Product product)
    {
        if (!await repo.Exists(product.Id))
            return NotFound();

       repo.Update(product);

        if (!await repo.SaveChangesAsync())
            return StatusCode(StatusCodes.Status500InternalServerError);

        return NoContent();
    }
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteProduct(Guid id)
    {
        Product? product =await repo.GetByIdAsync(id);
        if (product == null) return NotFound();

        repo.Remove(product);

        if (!await repo.SaveChangesAsync())
            return StatusCode(StatusCodes.Status500InternalServerError);

        return Ok();
    }
    [HttpGet("brands")]
    public async Task<ActionResult<List<string>>> GetBrands()
    {
        DistinctBrandsSpec distinctBrandsSpec = new DistinctBrandsSpec();
        IEnumerable<string>? brands =await repo.ListSpecAsync(distinctBrandsSpec);
        return Ok(brands);
    }
    [HttpGet("types")]
    public async Task<ActionResult<List<string>>> GetTypes()
    {
        return Ok();
    }
}
