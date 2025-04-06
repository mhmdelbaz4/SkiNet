using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);   
    }

    public async Task<IReadOnlyList<Product>> GetProducts(string? brand, string? type, string? sort)
    {
        IQueryable<Product> query = context.Products.AsQueryable();
        if (! string.IsNullOrWhiteSpace(brand))
            query = query.Where(p => p.Brand.Contains(brand));
        if (!string.IsNullOrWhiteSpace(type))
            query = query.Where(p => p.Type.Contains(type));

        query = sort switch
        {
            "priceAsc" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Name)
        };

        Console.WriteLine(query.ToQueryString());

        return await query.ToListAsync();
    }

    public async Task<List<string>> GetBrandsAsync()
    {
        List<string> brands = await context.Products.Select(x => x.Brand)
                                                    .Distinct()                                               
                                                    .ToListAsync();

        return brands;
    }

    public async Task<Product?> GetProductById(Guid id)
    {
        return await context.Products.FindAsync(id);
    }

    public async Task<List<string>> GetTypesAsync()
    {
        List<string> types = await context.Products.Select(x => x.Type)
                                                    .Distinct()
                                                    .ToListAsync();

        return types;
    }

    public async Task<bool> ProductExistsAsync(Guid id)
    {
        return await context.Products.AnyAsync(p => p.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return  await context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified; 
    }
}
