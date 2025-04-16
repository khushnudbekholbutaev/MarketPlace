using Microsoft.AspNetCore.Http;

namespace TechStation.Service.DTOs.Banners;

public class BannerForUpdateDto
{
    public List<IFormFile> Images { get; set; }
    public long BrendId { get; set; }
    public long CategoryId { get; set; }
}
