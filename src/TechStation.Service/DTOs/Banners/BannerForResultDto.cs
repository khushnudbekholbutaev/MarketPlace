using Microsoft.AspNetCore.Http;

namespace TechStation.Service.DTOs.Banners;

public class BannerForResultDto
{
    public long Id { get; set; }
    public string NameUz { get; set; }
    public string NameRu { get; set; }
    public string  Images { get; set; }
    public long BrendId { get; set; }
    public long CategoryId { get; set; }
}
