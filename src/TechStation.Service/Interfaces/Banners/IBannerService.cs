using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Banners;

namespace TechStation.Service.Interfaces.Banners;

public interface IBannerService
{
    Task<int> CountAsync();
    Task<bool> RemoveAsync(long id);
    Task<BannerForResultDto> RetrieveByIdAsync(long id);
    Task<BannerForResultDto> GetBannerName(string name);
    Task<BannerForResultDto> AddAsync(BannerForCreationDto dto);
    Task<BannerForResultDto> ModifyAsync(long id,BannerForUpdateDto dto);
    Task<ICollection<BannerForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
