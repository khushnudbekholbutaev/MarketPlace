using System.ComponentModel.DataAnnotations;
using TechStation.Domain.Commons;

namespace TechStation.Domain.Entities;

public class Order : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public decimal TotalAmount { get; set; }
    public int Quantity { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}
