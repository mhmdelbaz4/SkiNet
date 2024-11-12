using Microsoft.EntityFrameworkCore;
using SkiNet_API.Entities;
using SkiNet_API.IRepos;
using SkiNet_API.Specifications;
using System.Linq.Expressions;

namespace SkiNet_API.Repos;

public class GenericRepo<T>(StoreContext context) : IGenericRepo<T> where T : BaseEntity
{
    public void Add(T entity)
    {
        context.Set<T>().Add(entity);
    }

    public bool Exists(int id)
    {
        bool isExisting =context.Set<T>().Any(x => x.Id == id);
        return isExisting;
    }

    public void Delete(T entity)
    {
        context.Set<T>().Remove(entity);
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        IReadOnlyList<T> records = await context.Set<T>().ToListAsync();
        return records;
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        T? record = await context.Set<T>().FindAsync(id);
        return record;
    }

    public async Task<bool> SaveChangesAsync()
    {
        bool SavedSuccessfully =await context.SaveChangesAsync() > 0;
        return SavedSuccessfully;
    }

    public void Update(T entity)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<T?> GetEntityWithSpecAsync(IBaseSpecification<T> spec)
    {
        T? entity =await EvaluateSpecification(spec).FirstOrDefaultAsync();
        return entity;
    }

    public async Task<IReadOnlyList<T>> GetEntitiesWithSpecAsync(IBaseSpecification<T> spec)
    {
        IReadOnlyList<T> entities = await EvaluateSpecification(spec).ToListAsync();
        return entities;
    }
    public async Task<IReadOnlyList<TResult>?> GetEntitiesWithSpecAsync<TResult>(IBaseSpecification<T, TResult> spec)
    {
        IReadOnlyList<TResult>? entities = await EvaluateSpecification<TResult>(spec).ToListAsync<TResult>();
        return entities;

    }
    public async Task<TResult?> GetEntityWithSpecAsync<TResult>(IBaseSpecification<T, TResult> spec)
    {
        TResult? entity = await EvaluateSpecification<TResult>(spec).FirstOrDefaultAsync();
        return entity;
    }

    private IQueryable<T> EvaluateSpecification(IBaseSpecification<T> spec)
    {
        return SpecificationEvaluator<T>.GetQuery(context.Set<T>(), spec);
    }
    private IQueryable<TResult> EvaluateSpecification<TResult>(IBaseSpecification<T, TResult> spec)
    {
        return SpecificationEvaluator<T>.GetQuery<TResult>(context.Set<T>(),spec);
    }

    public async Task<int> CountAsync(IBaseSpecification<T> baseSpecification)
    {
        IQueryable<T> query = SpecificationEvaluator<T>.ApplyCriteria(context.Set<T>(), baseSpecification);
        return await query.CountAsync();
    }
}
