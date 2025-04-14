using TechStation.Service.DTOs.Orders;

namespace TechStation.Service.DTOs.OrderDetails;

public class OrderDetailForResultDto
{
    public long Id { get; set; }
    public long OrderId { get; set; }
    public long PaymentId { get; set; }
    public decimal TotalAmount { get; set; }
    public int Quantity { get; set; }
}
