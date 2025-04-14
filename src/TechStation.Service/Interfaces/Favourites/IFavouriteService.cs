using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.CartItems;
using TechStation.Service.DTOs.Favourites;

namespace TechStation.Service.Interfaces.Favourites;

public interface IFavouriteService
{
    public Task<int> CountAsync();
    Task<bool> ClearDataAsync(bool clear);
    public Task<bool> RemoveAsync(long id,bool token);
    public Task<FavouriteForResultDto> RetrieveByIdAsync(long id);
    public Task<bool> ClearAllFavouritesAsync(bool clear = false);
    public Task<FavouriteForResultDto> AddAsync(FavouriteForCreationDto dto,bool token);
    public Task<ICollection<FavouriteForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
