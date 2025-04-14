using Microsoft.AspNetCore.Http;

namespace TechStation.Service.DTOs.Banners;

public class BannerForCreationDto
{
    public string NameUz { get; set; }
    public string NameRu { get; set; }                 
    public List<IFormFile> Images { get; set; }
    public long BrendId { get; set; }
    public long CategoryId { get; set; }
}
