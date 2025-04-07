namespace Core.Interfaces;
public interface ISpecification<T> where T : BaseEntity
{
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, Object>>? SortBy { get; }
    Expression<Func<T, Object>>? SortByDescending { get; }
    bool Distinct { get; }
    int Take { get; }
    int Skip { get; }
    bool IsPagingEnabled { get; }
    IQueryable<T> ApplyCriteria(IQueryable<T> inputQuery);
}
public interface ISpecification<T,TResult> : ISpecification<T> where T: BaseEntity
{
    Expression<Func<T,TResult>>? Select { get; }
}