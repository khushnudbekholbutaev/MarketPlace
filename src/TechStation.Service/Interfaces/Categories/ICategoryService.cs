using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Categories;

namespace TechStation.Service.Interfaces.Categories;

public interface ICategoryService
{
    Task<int> CountAsync();
    Task<bool> RemoveAsync(long id);
    Task<CategoryForResultDto> RetrieveByIdAsync(long id);
    Task<CategoryForResultDto> AddAsync(CategoryForCreationDto dto);
    Task<CategoryForResultDto> ModifyAsync(long id,CategoryForUpdateDto dto);
    Task<IEnumerable<CategoryForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
