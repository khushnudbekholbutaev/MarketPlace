using TechStation.Domain.Commons;

namespace TechStation.Domain.Entities;

public class Favourite : Auditable
{
    public long UserId { get; set; }
    public long ProductId { get; set; }
    public User User { get; set; }
    public Product Product { get; set; }
}
