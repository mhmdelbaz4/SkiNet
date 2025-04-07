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

        Expression<Func<Product, object>> sortExpression;
        if (specParams.SortOption == SortingOptionsEnum.Asc)
        {
            sortExpression = specParams.SortBy switch
            {
                ProductSortEnum.Name => p => p.Name,
                ProductSortEnum.Price => p => p.Price,
                _ => p => p.Name
            };
            ApplySort(sortExpression);
        }
        else
        {
            sortExpression = specParams.SortBy switch
            {
                ProductSortEnum.Name => p => p.Name,
                ProductSortEnum.Price => p => p.Price,
                _ => p => p.Name
            };
            ApplySortDescending(sortExpression);
        }

        ApplyPaging(specParams.PageIndex, specParams.PageSize);
    }
}
