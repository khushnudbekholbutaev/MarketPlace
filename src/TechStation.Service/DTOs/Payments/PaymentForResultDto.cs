using TechStation.Service.DTOs.OrderDetails;
using TechStation.Service.DTOs.Orders;

namespace TechStation.Service.DTOs.Payments;

public class PaymentForResultDto
{
    public long Id { get; set; }
    public string PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public ICollection<OrderDetailForResultDto> OrderDetails { get; set; }
}
