namespace Infrastructure.Data;
public class SeedDataContext
{
    public static async Task SeedDataAsync(AppDbContext context)
    {
        if (!context.Products.Any())
        {
            string productsData = File.ReadAllText("../Infrastructure/Data/SeedData/products.json");
            List<Product>? products = JsonSerializer.Deserialize<List<Product>>(productsData);
            if(products == null) return;

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}
