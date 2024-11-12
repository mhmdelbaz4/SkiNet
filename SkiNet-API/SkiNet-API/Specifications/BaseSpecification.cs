using System.Linq.Expressions;

namespace SkiNet_API.Specifications;

public class BaseSpecification<T> : IBaseSpecification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; protected set; }
    public Expression<Func<T, object>>? OrderByExpression { get; protected set; }
    public Expression<Func<T, object>>? OrderByDescendingExpression { get; protected set; }

    public int PageIndex { get; protected set; }

    public int PageSize { get; protected set; }

    public bool IsPaginationEnabled { get; protected set; }

    public void ApplyPagination(int pageIndex, int pageSize)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
        IsPaginationEnabled = true;
    }
}
public class BaseSpecification<T, TResult> : BaseSpecification<T>, IBaseSpecification<T, TResult>
{
    public Expression<Func<T, TResult>>? Select { get; protected set; }
    public bool IsDistinct { get; protected set; }
}
