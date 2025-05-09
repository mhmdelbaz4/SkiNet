using Core.Enums;

namespace Core.Specifications.Params;
public class ProductSpecParams
{
    private const int defaultPageSize = 10;
    private int _pageSize = defaultPageSize;
    private int _pageIndex = 1;
    public int PageSize
    {
        get => _pageSize;
        set
        {
            _pageSize = value > 0 ? value : defaultPageSize;
        }
    }
    public int PageIndex 
    {
        get=> _pageIndex;
        set 
        {
            _pageIndex = value > 0 ? value : 1;
        }
    }
    private List<string> _brands = [];
    public List<string> Brands{ get => _brands; set
        {
            _brands = value.SelectMany(b => b.Split(',')).ToList();
        }
    }

    private List<string> _types = [];
    public List<string> Types
    {
        get => _types; set
        {
            _types = value.SelectMany(b => b.Split(',')).ToList();
        }
    }

    private string _search = "";
    public string Search 
    {
        get => _search;
        set
        {
            _search = value != null ? value.ToLower() : "";   
        }
    }
    public ProductSortEnum  SortBy { get; set; }
}
