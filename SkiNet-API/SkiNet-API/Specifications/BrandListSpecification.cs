using SkiNet_API.Entities;
namespace SkiNet_API.Specifications;

public class BrandListSpecification : BaseSpecification<Product, string>
{
    public BrandListSpecification()
    {
        Select = p => p.Brand;
        IsDistinct = true;
    }
}
