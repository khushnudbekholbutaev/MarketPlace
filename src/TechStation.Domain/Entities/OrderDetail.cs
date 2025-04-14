using TechStation.Domain.Entities;
using TechStation.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace TechStation.Domain.Entities;

public class OrderDetail : Auditable
{
    public long OrderId { get; set; }
    public Order Order { get; set; }
    public long PaymentId { get; set; }
    public Payment Payment { get; set; }
    public int Quantity { get; set; }
    public decimal TotalAmount { get; set; }
}
