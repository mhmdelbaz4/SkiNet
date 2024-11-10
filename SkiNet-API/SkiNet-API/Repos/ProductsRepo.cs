using Microsoft.EntityFrameworkCore;
using SkiNet_API.Entities;
using SkiNet_API.IRepos;
using System.Diagnostics;

namespace SkiNet_API.Repos
{
    public class ProductsRepo(StoreContext context) : IProductsRepo
    {
        private readonly StoreContext _storeContext = context;

        public void AddProduct(Product product)
        {
            _storeContext.Products.Add(product);
        }

        public void DeleteProduct(Product product)
        {
            _storeContext.Products.Remove(product);
        }
        public void UpdateProduct(Product product)
        {
            _storeContext.Entry(product).State = EntityState.Modified;
        }
        public async Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort)
        {
            IQueryable<Product> query = _storeContext.Products.AsQueryable();
            if (!String.IsNullOrWhiteSpace(brand))
                query = query.Where(p => p.Brand == brand);
            if (!String.IsNullOrWhiteSpace(type))
                query = query.Where(p => p.Type == type);

            query = sort switch
            {
                "priceAsc" => query.OrderBy(p => p.Price),
                "priceDesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Name)
            };

            IReadOnlyList<Product> products = await query.ToListAsync();
            return products;     
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _storeContext.Products.FindAsync(id);
        }

        public bool ProductExists(int id)
        {
           return _storeContext.Products.Any(p => p.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _storeContext.SaveChangesAsync()>0;
        }

        public async Task<IReadOnlyList<string>> GetAllBrands()
        {
            IReadOnlyList<string> brands =await _storeContext.Products.Select(p => p.Brand)
                                                        .Distinct()
                                                        .ToListAsync();
            return brands;
        }

        public async Task<IReadOnlyList<string>> GetAllTypes()
        {
            IReadOnlyList<string> types = await _storeContext.Products.Select(p => p.Type)
                                                                  .Distinct()
                                                                  .ToListAsync();
            return types;
        }
    }
}
