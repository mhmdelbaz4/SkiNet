namespace Core.Specifications;

public class DistinctTypesSpec : BaseSpecification<Product, string>
{
    public DistinctTypesSpec()
    {
        ApplySelect(p => p.Type);
        IsDistinct();
    }
}
