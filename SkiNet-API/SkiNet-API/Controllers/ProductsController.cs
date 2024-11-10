using Microsoft.AspNetCore.Mvc;
using SkiNet_API.Entities;
using SkiNet_API.IRepos;
using SkiNet_API.Specifications;

namespace SkiNet_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IGenericRepo<Product> productsRepo) : ControllerBase 
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string brand, string type, string sort)
    {
        ProductSpecification specification = new ProductSpecification(brand, type,sort);
        IReadOnlyList<Product> products = await productsRepo.GetEntitiesWithSpecAsync(specification);
        return Ok(products);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        Product product = await productsRepo.GetByIdAsync(id);
        if (product is null)
            return NotFound();

        return Ok(product);
    }
    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct(Product product)
    {
        if (product == null)
            return BadRequest();

        productsRepo.Add(product);
        bool addSuccessfully = await productsRepo.SaveChangesAsync();
        if (!addSuccessfully)
            return BadRequest();

        return Created();
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        bool isExisting = productsRepo.Exists(id);
        if (id != product.Id || !isExisting)
            return NotFound();

        productsRepo.Update(product);
        bool updatedSuccessfully = await productsRepo.SaveChangesAsync();
        if (!updatedSuccessfully)
            return BadRequest();

        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        Product product = await productsRepo.GetByIdAsync(id);
        if (product is null)
            return NotFound();

        productsRepo.Delete(product);
        bool deletedSuccessfully = await productsRepo.SaveChangesAsync();
        if (!deletedSuccessfully)
            return BadRequest();

        return NoContent();
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetAllBrands()
    {
        BrandListSpecification brandSpecification = new BrandListSpecification();
        IReadOnlyList<string>? brands =await productsRepo.GetEntitiesWithSpecAsync(brandSpecification);
        return Ok(brands);
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetAllTypes()
    {
        TypeListSpecification typeListSpecification = new TypeListSpecification();
        IReadOnlyList<string>? types = await productsRepo.GetEntitiesWithSpecAsync(typeListSpecification);
        return Ok(types);
    }
}
