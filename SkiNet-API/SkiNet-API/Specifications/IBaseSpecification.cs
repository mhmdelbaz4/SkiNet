using System.Linq.Expressions;

namespace SkiNet_API.Specifications;

public interface IBaseSpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; }
    public Expression<Func<T, object>>? OrderByExpression { get; }
    public Expression<Func<T, object>>? OrderByDescendingExpression { get; }
    public int PageIndex {  get; }
    public int PageSize { get; }
    public bool IsPaginationEnabled { get;}
}
public interface IBaseSpecification<T, TResult> : IBaseSpecification<T>
{
    public Expression<Func<T, TResult>>? Select { get; }
    public bool IsDistinct { get; }
}

