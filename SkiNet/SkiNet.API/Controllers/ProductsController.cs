namespace API.Controllers;
public class ProductsController(IGenericRepository<Product> repo) : APIControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
    {
        ProductSpecification spec= new ProductSpecification(productSpecParams);

        Pagination<Product> pagination =await GetPaginationResult(repo, spec, productSpecParams.PageIndex, productSpecParams.PageSize);
        return Ok(pagination);
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
        DistinctTypesSpec distinctTypesSpec = new DistinctTypesSpec();
        IEnumerable<string>? types = await repo.ListSpecAsync(distinctTypesSpec);
        return Ok(types);
    }
}
