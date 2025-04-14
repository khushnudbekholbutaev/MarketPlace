using TechStation.Service.DTOs.Products;

namespace TechStation.Service.DTOs.CartItems;

public class CartItemForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public decimal Quantity { get; set; }
    //public ICollection<ProductForResultDto> Products { get; set; }
}
