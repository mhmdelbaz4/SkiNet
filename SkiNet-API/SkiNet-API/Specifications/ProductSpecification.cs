using SkiNet_API.Entities;
using SkiNet_API.IRepos;
using System.Linq.Expressions;

namespace SkiNet_API.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(string brand, string type, string sort)
    {
        Criteria = p => (string.IsNullOrWhiteSpace(brand) || p.Brand == brand)
                         && (string.IsNullOrWhiteSpace(type) || p.Type == type);

        switch (sort)
            {
                case "priceAsc":
                    OrderByExpression = p => p.Price;
                    break;
                case "priceDesc":
                    OrderByDescendingExpression = p => p.Price;
                    break;
                default:
                    OrderByExpression = p => p.Name;
                    break;
            }
    }
}
