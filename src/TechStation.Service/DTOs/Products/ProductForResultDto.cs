using TechStation.Service.DTOs.Categories;
using TechStation.Service.DTOs.Orders;

namespace TechStation.Service.DTOs.Products;

public class ProductForResultDto
{
    public long Id { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public long BrendId { get; set; }
    public string Images { get; set; }
}
