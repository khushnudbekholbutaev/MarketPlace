namespace TechStation.Service.DTOs.OrderDetails;

public class OrderDetailForCreationDto
{
    public long OrderId { get; set; }
    public long PaymentId { get; set; }
    public decimal TotalAmount { get; set; }
    public int Quantity { get; set; }
}
