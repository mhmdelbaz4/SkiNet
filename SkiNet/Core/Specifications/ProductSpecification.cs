using Core.Enums;

namespace Core.Specifications;
public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(string brand, string type)
    {
        if(!string.IsNullOrWhiteSpace(type) && !string.IsNullOrWhiteSpace(brand))
        {
            ApplyFilter( p =>(string.IsNullOrEmpty(type) || p.Type.Contains(type)) &&
                           (string.IsNullOrEmpty(brand) || p.Brand.Contains(brand)));
        }else if (!string.IsNullOrWhiteSpace(type))
        {
            ApplyFilter(p => (string.IsNullOrEmpty(type) || p.Type.Contains(type)));
        }else if (!string.IsNullOrWhiteSpace(brand))
        {
            ApplyFilter(p => (string.IsNullOrEmpty(brand) || p.Type.Contains(brand)));
        }
    }

    public ProductSpecification(string brand, string type, ProductSortEnum sortBy, SortingOptionsEnum sortOption)
                : this(brand, type)
    {
        Expression<Func<Product, object>> sortExpression;
        if (sortOption == SortingOptionsEnum.Asc)
        {
            sortExpression = sortBy switch
            {
                ProductSortEnum.Name => p => p.Name,
                ProductSortEnum.Price => p => p.Price,
                _ => p => p.Name
            };
            ApplySort(sortExpression);
        }
        else
        {
            sortExpression = sortBy switch
            {
                ProductSortEnum.Name => p => p.Name,
                ProductSortEnum.Price => p => p.Price,
                _ => p => p.Name
            };
            ApplySortDescending(sortExpression);
        }
    }
}
