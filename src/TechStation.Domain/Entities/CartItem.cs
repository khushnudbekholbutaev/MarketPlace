using TechStation.Domain.Commons;

namespace TechStation.Domain.Entities;

public class CartItem : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; }
    public decimal Quantity { get; set; }

}
