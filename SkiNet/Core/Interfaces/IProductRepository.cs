using Core.Entities;

namespace Core.Interfaces;
public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProducts(string? brand, string? type, string? sort);
    Task<Product?> GetProductById(Guid id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    Task<bool> ProductExistsAsync(Guid id);
    Task<bool> SaveChangesAsync();
    Task<List<string>> GetTypesAsync();
    Task<List<string>> GetBrandsAsync();
}
