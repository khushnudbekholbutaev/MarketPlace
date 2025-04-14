using TechStation.Service.DTOs.Categories;

namespace TechStation.Service.DTOs.Catalogs;

public class CatalogForResultDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<CategoryForResultDto> Categories { get; set; }
}
