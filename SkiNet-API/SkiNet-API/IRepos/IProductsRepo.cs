using SkiNet_API.Entities;

namespace SkiNet_API.IRepos
{
    public interface IProductsRepo
    {
        Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);   
        Task<Product?> GetProductByIdAsync(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(Product product);
        bool ProductExists(int id);
        Task<bool> SaveChangesAsync();

        Task<IReadOnlyList<string>> GetAllBrands();
        Task<IReadOnlyList<string>> GetAllTypes();

    }
}
