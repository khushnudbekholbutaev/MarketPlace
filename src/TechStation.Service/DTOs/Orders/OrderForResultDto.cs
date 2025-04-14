using TechStation.Service.DTOs.OrderDetails;

namespace TechStation.Service.DTOs.Orders;

public class OrderForResultDto
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public decimal TotalAmount { get; set; }
    public int Quantity { get; set; }
    public ICollection<OrderDetailForResultDto> OrderDetails { get; set;}
}
