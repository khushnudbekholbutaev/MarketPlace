namespace TechStation.Service.DTOs.CartItems;

public class CartItemForCreationDto
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public decimal Quantity { get; set; }
}
