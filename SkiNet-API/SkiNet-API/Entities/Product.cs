using System.ComponentModel.DataAnnotations;

namespace SkiNet_API.Entities;

public class Product : BaseEntity
{
    [StringLength(250)]
    public required string Name { get; set; }
    [StringLength(2500)]
    public required string Description { get; set; }
    public decimal Price { get; set; }

    [StringLength(250)]
    public required string PictureUrl { get; set; }

    [Range(minimum:1,maximum:int.MaxValue)]
    public int QuantityInStock{ get; set; }
    [StringLength(250)]
    public required string Type { get; set; }
    [StringLength(250)]
    public required string Brand { get; set; }
}
