namespace Core.Entities;
public class Product : BaseEntity
{
    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public required string PictureUrl { get; set; }
    public int QuantityInStock { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
}
