using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Catalogs;

namespace TechStation.Service.Interfaces.Catalogs;

public interface ICatalogService
{
    Task<int> CountAsync();
    Task<bool> RemoveAsync(long id);
    Task<CatalogForResultDto> RetrieveByIdAsync(long id);
    Task<CatalogForResultDto> AddAsync(CatalogForCreationDto dto);
    Task<CatalogForResultDto> ModifyAsync(long id, CatalogForUpdateDto dto);
    Task<IEnumerable<CatalogForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
