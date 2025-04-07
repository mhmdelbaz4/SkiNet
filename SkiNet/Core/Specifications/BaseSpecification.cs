
namespace Core.Specifications;
public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
{
    public Expression<Func<T, bool>>? Criteria { get; private set; }
    public Expression<Func<T, object>>? SortBy { get; private set; }
    public Expression<Func<T, object>>? SortByDescending { get; private set; }
    public bool Distinct { get; private set; }
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }
    public void ApplyFilter(Expression<Func<T, bool>> criteria) => this.Criteria = criteria;
    public void ApplySort(Expression<Func<T, object>> sortBy) => this.SortBy = sortBy;
    public void ApplySortDescending(Expression<Func<T, object>> sortByDesc) => this.SortByDescending = sortByDesc;
    public void IsDistinct() => this.Distinct = true;
    public void ApplyPaging(int pageIndex, int pageSize)
    {
        Skip = (pageIndex - 1) * pageSize;
        Take = pageSize;
        this.IsPagingEnabled = true;
    }

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if(query != null && Criteria != null)
            query = query.Where(Criteria);

        return query!;
    }
}
public class BaseSpecification<T, TResult> : BaseSpecification<T>, ISpecification<T, TResult> where T : BaseEntity
{
    public Expression<Func<T, TResult>>? Select { get; private set; }

    public void ApplySelect(Expression<Func<T, TResult>> select) => this.Select = select;
}