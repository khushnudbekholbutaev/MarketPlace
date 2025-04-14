using TechStation.Service.DTOs.Products;

namespace TechStation.Service.DTOs.Brends;

public class BrendForResultDto
{
    public long Id { get; set; }
    public string BrendName { get; set; }
    public  ICollection<ProductForResultDto> Products { get; set; }
}
