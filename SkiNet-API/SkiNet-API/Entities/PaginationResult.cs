namespace SkiNet_API.Entities;

public class PaginationResult<T>(int index,int size,int count,IReadOnlyList<T> data)
{
    public int PageIndex { get; set; } = index;
    public int PageSize { get; set; } = size;

    public int Count { get; set; } = count;

    public IReadOnlyList<T>? Data { get; set; } = data;
}
