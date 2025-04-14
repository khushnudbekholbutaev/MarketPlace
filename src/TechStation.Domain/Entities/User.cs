using System.ComponentModel.DataAnnotations;
using TechStation.Domain.Commons;
using TechStation.Domain.Enums;

namespace TechStation.Domain.Entities;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public Role Role { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public ICollection<Favourite> Favourites { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
}
