namespace Core.Specifications;
public class DistinctBrandsSpec : BaseSpecification<Product, string>
{
    public DistinctBrandsSpec()
    {
        ApplySelect(p => p.Brand);
        IsDistinct();
    }
}
