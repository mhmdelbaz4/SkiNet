using SkiNet_API.Entities;
using System.Text.Json;

namespace SkiNet_API.Data;

public static class StoreContextSeed
{
    public static async Task SeedProductsAsync(StoreContext context)
    {
        if(!context.Products.Any())
        {
            string products = await File.ReadAllTextAsync("./Data/SeedData/Products.json");
            IEnumerable<Product> productsEnumerable = JsonSerializer.Deserialize<IEnumerable<Product>>(products);
            context.Products.AddRange(productsEnumerable);
            context.SaveChanges();
        }
    }
}
