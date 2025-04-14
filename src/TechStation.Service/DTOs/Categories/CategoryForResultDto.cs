using TechStation.Service.DTOs.Products;

namespace TechStation.Service.DTOs.Categories;

public class CategoryForResultDto
{
    public long Id { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }
    public long CatalogId { get; set; }
    public ICollection<ProductForResultDto> Products { get; set; }
}
