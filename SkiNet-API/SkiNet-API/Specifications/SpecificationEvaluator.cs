using Microsoft.EntityFrameworkCore;

namespace SkiNet_API.Specifications;

public static class SpecificationEvaluator<T>
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, IBaseSpecification<T> spec)
    {
        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);
        if (spec.OrderByExpression != null)
            query = query.OrderBy(spec.OrderByExpression);
        if (spec.OrderByDescendingExpression != null)
            query = query.OrderByDescending(spec.OrderByDescendingExpression);
        
        if(spec.IsPaginationEnabled)
            query = query.Skip(spec.PageIndex * spec.PageSize).Take(spec.PageSize);

        Console.WriteLine(query.ToQueryString());
        return query;
    }

    public static IQueryable<TResult> GetQuery<TResult>(IQueryable<T> query, IBaseSpecification<T, TResult> spec)
    {
        IQueryable<T> queryResult = GetQuery(query, spec as IBaseSpecification<T>);

        IQueryable<TResult>? queryWithSelect = queryResult as IQueryable<TResult>;
        if(spec.Select !=null)
            queryWithSelect = queryResult.Select(spec.Select);
        if(spec.IsDistinct)
            queryWithSelect = queryWithSelect?.Distinct();

        return queryWithSelect ?? query.Cast<TResult>();
    }

    public static IQueryable<T> ApplyCriteria(IQueryable<T> query, IBaseSpecification<T> baseSpecification)
    {
        if(baseSpecification.Criteria != null)
            query = query.Where(baseSpecification.Criteria);
        Console.WriteLine(query.ToQueryString());
        return query;
    }

}
