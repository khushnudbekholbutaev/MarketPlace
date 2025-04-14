using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Brends;

namespace TechStation.Service.Interfaces.Brends;

public interface IBrendService
{
    Task<int> CountAsync();
    Task<bool> RemoveAsync(long id);
    Task<BrendForResultDto> RetrieveByIdAsync(long id);
    Task<BrendForResultDto> AddAsync(BrendForCreationDto dto);
    Task<BrendForResultDto> ModifyAsync(long id, BrendForUpdateDto dto);
    Task<ICollection<BrendForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
