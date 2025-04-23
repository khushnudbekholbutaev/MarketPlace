using TechStation.Domain.Entities;
using TechStation.Domain.Enums;
using TechStation.Service.DTOs.CartItems;
using TechStation.Service.DTOs.Favourites;
using TechStation.Service.DTOs.Orders;

namespace TechStation.Service.DTOs.Users;

public class UserForResultDto
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<OrderForResultDto> Orders { get; set; }
    public ICollection<RefreshToken> RefreshTokens { get; set; }
    public ICollection<CartItemForResultDto> CartItems { get; set; }
    public ICollection<FavouriteForResultDto> Favourites { get; set; }
}
