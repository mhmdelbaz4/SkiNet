using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiNet_API.Entities;
using SkiNet_API.IRepos;

namespace SkiNet_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(IProductsRepo productsRepo) : ControllerBase 
{
    private readonly IProductsRepo _productsRepo = productsRepo;
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts(string? brand, string? type, string? sort)
    {
        IReadOnlyList<Product> products = await _productsRepo.GetProductsAsync(brand,type,sort);
        return Ok(products);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        Product product = await _productsRepo.GetProductByIdAsync(id);
        if (product is null)
            return NotFound();

        return Ok(product);
    }
    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct(Product product)
    {
        if (product == null)
            return BadRequest();

        _productsRepo.AddProduct(product);
        bool addSuccessfully = await _productsRepo.SaveChangesAsync();
        if (!addSuccessfully)
            return BadRequest();

        return Created();
    }
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        bool isExisting = _productsRepo.ProductExists(id);
        if (id != product.Id || !isExisting)
            return NotFound();

        _productsRepo.UpdateProduct(product);
        bool updatedSuccessfully = await _productsRepo.SaveChangesAsync();
        if (!updatedSuccessfully)
            return BadRequest();

        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        Product product = await _productsRepo.GetProductByIdAsync(id);
        if (product is null)
            return NotFound();

        _productsRepo.DeleteProduct(product);
        bool deletedSuccessfully = await _productsRepo.SaveChangesAsync();
        if (!deletedSuccessfully)
            return BadRequest();

        return NoContent();
    }
    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetAllBrands()
    {
        IReadOnlyList<string> brands =await _productsRepo.GetAllBrands();
        return Ok(brands);
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetAllTypes()
    {
        IReadOnlyList<string> types =await _productsRepo.GetAllTypes();
        return Ok(types);
    }

}
