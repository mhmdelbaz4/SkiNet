using SkiNet_API.Entities;
using SkiNet_API.IRepos;
using System.Linq.Expressions;

namespace SkiNet_API.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams productSpecParams)
    {
        Criteria = p =>   (string.IsNullOrWhiteSpace(productSpecParams.Search) || p.Name.Contains(productSpecParams.Search))&&
                          ( productSpecParams.Brands.Count==0 || productSpecParams.Brands.Contains(p.Brand) )
                         && (productSpecParams.Types.Count == 0 || productSpecParams.Types.Contains(p.Type));

        if(PageSize > 0)
            ApplyPagination(productSpecParams.PageIndex, productSpecParams.PageSize);

        //switch (sort)
        //    {
        //        case "priceAsc":
        //            OrderByExpression = p => p.Price;
        //            break;
        //        case "priceDesc":
        //            OrderByDescendingExpression = p => p.Price;
        //            break;
        //        default:
        //            OrderByExpression = p => p.Name;
        //            break;
        //    }
    }

}
