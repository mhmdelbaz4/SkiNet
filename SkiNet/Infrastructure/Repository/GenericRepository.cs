using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class GenericRepository<T>(AppDbContext context) : IGenericRepository<T> where T : BaseEntity
{
    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public async Task<bool> Exists(Guid id)
    {
        return await context.Set<T>().AnyAsync(entity => entity.Id == id);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<T?> GetEntitySpecAsync(ISpecification<T> spec)
    {
        IQueryable<T> query = SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<TResult?> GetEntitySpecAsync<TResult>(ISpecification<T, TResult> spec)
    {
        IQueryable<TResult> query = SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
        return await query.FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<T>?> ListSpecAsync(ISpecification<T> spec)
    {
        IQueryable<T> query = SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<TResult>?> ListSpecAsync<TResult>(ISpecification<T, TResult> spec)
    {
        IQueryable<TResult> query = SpecificationEvaluator<T>.GetQuery(context.Set<T>().AsQueryable(), spec);
        return await query.ToListAsync();
    }

    public void Remove(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Update(T entity)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }
}
