namespace Core.Interfaces;
public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetEntitySpecAsync(ISpecification<T> spec);
    Task<IReadOnlyList<T>?> ListSpecAsync(ISpecification<T> spec);
    Task<TResult?> GetEntitySpecAsync<TResult>(ISpecification<T, TResult> spec);
    Task<IReadOnlyList<TResult>?> ListSpecAsync<TResult>(ISpecification<T, TResult> spec);
    void Add(T entity);
    void Update(T entity);
    void Remove(T entity);
    Task<bool> SaveChangesAsync();
    Task<bool> Exists(Guid id);
}
