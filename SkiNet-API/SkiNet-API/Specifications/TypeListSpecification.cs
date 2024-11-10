using SkiNet_API.Entities;

namespace SkiNet_API.Specifications;

public class TypeListSpecification : BaseSpecification<Product, string>
{
    public TypeListSpecification()
    {
        Select = p => p.Type;
        IsDistinct = true;
    }
}
