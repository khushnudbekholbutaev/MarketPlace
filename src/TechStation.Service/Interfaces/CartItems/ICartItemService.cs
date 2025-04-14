using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.CartItems;

namespace TechStation.Service.Interfaces.CartItems;

public interface ICartItemService
{
    Task<int> CountAsync();
    Task<bool> RemoveAsync(long id,bool token);
    //Task<CartItemForResultDto> RetrieveByIdAsync(long id);
    public Task<bool> ClearAllCartItemsAsync(bool clear = false);
    //Task<CartItemForResultDto> ModifyAsync(long id,CartItemForUpdateDto dto);
    Task<ICollection<CartItemForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<CartItemForResultDto> AddAsync(CartItemForCreationDto dto,bool token,string? operation = null);

}
