using Microsoft.AspNetCore.Http;

namespace TechStation.Service.DTOs.Products;

public class ProductForCreationDto
{
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public long CategoryId { get; set; }
    public long BrendId { get; set; }   
    public List<IFormFile> Images { get; set; }
}
