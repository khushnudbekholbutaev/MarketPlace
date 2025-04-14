using TechStation.Domain.Enums;
using TechStation.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace TechStation.Domain.Entities;

public class Payment : Auditable
{
    public string PaymentMethod { get; set; }
    public decimal Amount { get; set; }
    public Status Status { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}
