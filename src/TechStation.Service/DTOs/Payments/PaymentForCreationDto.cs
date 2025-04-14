namespace TechStation.Service.DTOs.Payments;

public class PaymentForCreationDto
{
    public string PaymentMethod { get; set; }
    public decimal Amount { get; set; }
}
