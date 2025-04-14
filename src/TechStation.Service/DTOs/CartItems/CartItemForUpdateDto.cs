namespace TechStation.Service.DTOs.CartItems;

public class CartItemForUpdateDto
{
    public decimal Quantity { get; set; }
    public long UserId { get; set; }
    public long ProduId { get; set; }
}
