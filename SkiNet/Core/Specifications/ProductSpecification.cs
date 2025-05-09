using Core.Enums;
using Core.Specifications.Params;

namespace Core.Specifications;
public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecParams specParams)
    {
        Expression<Func<Product, bool>> criteriaExpression;
        if (specParams.Brands.Count > 0 && specParams.Types.Count > 0)
        {
            criteriaExpression = p => (specParams.Brands.Contains(p.Brand)
                                        && specParams.Types.Contains(p.Type)
                                        && (string.IsNullOrWhiteSpace(specParams.Search) || p.Name.ToLower().Contains(specParams.Search)));
        }
        else if (specParams.Brands.Count > 0)
        {
            criteriaExpression = p => specParams.Brands.Contains(p.Brand)
                                && (string.IsNullOrWhiteSpace(specParams.Search) || p.Name.ToLower().Contains(specParams.Search));

        }
        else if (specParams.Types.Count > 0)
        {
            criteriaExpression = p => specParams.Types.Contains(p.Type)
                                && (string.IsNullOrWhiteSpace(specParams.Search) || p.Name.ToLower().Contains(specParams.Search));
        }else
        {
            criteriaExpression = p => (string.IsNullOrWhiteSpace(specParams.Search) || p.Name.ToLower().Contains(specParams.Search));
        }
        ApplyFilter(criteriaExpression);

        switch(specParams.SortBy)
        {
            case ProductSortEnum.Name:
                ApplySort(p => p.Name);
                break;
            case ProductSortEnum.PriceAsc:
                ApplySort(p => p.Price);
                break;
            case ProductSortEnum.PriceDesc:
                ApplySortDescending(p => p.Price);
                break;
            default:
                ApplySort(p => p.Name);
                break;
        }
        ApplyPaging(specParams.PageIndex, specParams.PageSize);
    }
}
