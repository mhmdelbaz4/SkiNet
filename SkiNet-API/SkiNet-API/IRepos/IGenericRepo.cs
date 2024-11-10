using SkiNet_API.Entities;
using SkiNet_API.Specifications;

namespace SkiNet_API.IRepos;

public interface IGenericRepo<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> GetAllAsync();
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> SaveChangesAsync();
    bool Exists(int id);
    Task<T?> GetEntityWithSpecAsync(IBaseSpecification<T> spec);
    Task<IReadOnlyList<T>> GetEntitiesWithSpecAsync(IBaseSpecification<T> spec);
    Task<TResult?> GetEntityWithSpecAsync<TResult>(IBaseSpecification<T,TResult> spec);
    Task<IReadOnlyList<TResult>?> GetEntitiesWithSpecAsync<TResult>(IBaseSpecification<T,TResult> spec);
}
    