using TechStation.Service.DTOs.Products;

namespace TechStation.Service.DTOs.Favourites;

public class FavouriteForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public ICollection<ProductForResultDto> Products { get; set; }
}
