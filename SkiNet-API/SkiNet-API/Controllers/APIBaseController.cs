using Microsoft.AspNetCore.Mvc;
using SkiNet_API.Entities;
using SkiNet_API.IRepos;
using SkiNet_API.Specifications;

namespace SkiNet_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIBaseController<T> : ControllerBase where T: BaseEntity
    {
        public async Task<ActionResult>  GetPaginationResult(IGenericRepo<T> repo, IBaseSpecification<T> spec,
                                        int pageIndex, int pageSize) 
        {
            IReadOnlyList<T> data = await repo.GetEntitiesWithSpecAsync(spec);
            int count = await repo.CountAsync(spec);
            PaginationResult<T> paginationResult = new(pageIndex, pageSize, count, data);
            return Ok(paginationResult);
        }
    }
}
