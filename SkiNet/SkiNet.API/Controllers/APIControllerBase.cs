namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class APIControllerBase : ControllerBase
{
    protected async Task<Pagination<T>> GetPaginationResult<T>(IGenericRepository<T> repo, ISpecification<T> spec,
                                                   int pageIndex, int pageSize) where T : BaseEntity
    {
        IReadOnlyList<T>? data =await repo.ListSpecAsync(spec);
        int totalCount =await repo.CountAsync(spec);
        Pagination<T> pagination = new Pagination<T>(pageIndex, pageSize, totalCount, data);
        return pagination;
    }
}
