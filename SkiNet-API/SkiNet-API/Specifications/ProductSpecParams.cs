namespace SkiNet_API.Specifications;

public class ProductSpecParams
{
    private List<string> _brands =[];
    private List<string> _types=[];
    private int maxPageSize = 50;
    private int pageSize;

    
    public List<string> Brands
    {
        get => _brands;
        set
        {
            _brands = value.SelectMany(b => b.Split(',',StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }
    public List<string> Types
    {
        get => _types;
        set
        {
            _types = value.SelectMany(b => b.Split(',', StringSplitOptions.RemoveEmptyEntries)).ToList();
        }
    }
    public string? Search { get; set; }
    public int PageIndex { get; set; }
    public int PageSize 
    { 
        get => pageSize; 
        set
        {
            pageSize = value > maxPageSize ? maxPageSize : value;      
        } 
    }
}
