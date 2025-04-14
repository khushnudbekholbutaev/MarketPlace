namespace TechStation.Service.DTOs.OrderDetails;

public class OrderDetailForUpdateDto
{
    public long OrderId { get; set; }
    public long PaymentId { get; set; }
    public decimal TotalAmount { get; set; }
    public int Quantity { get; set; }
}
