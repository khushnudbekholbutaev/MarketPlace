namespace TechStation.Service.DTOs.Orders;

public class OrderForUpdateDto
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public decimal TotalAmount { get; set; }
    public int Quantity { get; set; }
}
