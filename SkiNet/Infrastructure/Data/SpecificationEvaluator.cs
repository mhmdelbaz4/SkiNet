namespace Infrastructure.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> spec)
    {
        IQueryable<T> query = inputQuery;
        if (spec.Criteria != null)
          query = query.Where(spec.Criteria);
        
        if(spec.SortBy != null)
            query = query.OrderBy(spec.SortBy);

        if (spec.SortByDescending != null)
            query = query.OrderByDescending(spec.SortByDescending);

        if (spec.Distinct)
            query = query.Distinct();

        if (spec.IsPagingEnabled)
            query = query.Skip(spec.Skip).Take(spec.Take);

        return query;
    }

    public static IQueryable<TResult> GetQuery<TResult>(IQueryable<T> query, ISpecification<T,TResult> spec)
    {
        if (spec.Criteria != null)
            query = query.Where(spec.Criteria);

        if (spec.SortBy != null)
            query = query.OrderBy(spec.SortBy);

        if (spec.SortByDescending != null)
            query = query.OrderByDescending(spec.SortByDescending);


        IQueryable<TResult>? selectedQuery = query as IQueryable<TResult>;
        if (spec.Select != null)
            selectedQuery = query.Select(spec.Select);

        if (selectedQuery != null && spec.Distinct)
            selectedQuery = selectedQuery.Distinct();

        if (spec.IsPagingEnabled)
            selectedQuery = selectedQuery?.Skip(spec.Skip).Take(spec.Take);

        return selectedQuery ?? query.Cast<TResult>();

    }
}
