using TechStation.Domain.Commons;

namespace TechStation.Domain.Entities;

public class RefreshToken : Auditable
{
    public string Token { get; set; }
    public DateTime ExpiryDate { get; set; }
    public long UserId { get; set; }
    public User User { get; set; }
}
