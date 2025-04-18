using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Brends;
using TechStation.Service.DTOs.Products;

namespace TechStation.Service.Interfaces.Brends;

public interface IBrendService
{
    Task<int> CountAsync();
    Task<bool> RemoveAsync(long id);
    Task<BrendForResultDto> RetrieveByIdAsync(long id);
    Task<BrendForResultDto> AddAsync(BrendForCreationDto dto);
    Task<BrendForResultDto> ModifyAsync(long id, BrendForUpdateDto dto);
    Task<ICollection<BrendForResultDto>> RetrieveAllAsync(PaginationParams @params);
    Task<ICollection<ProductForResultDto>> RetrieveAllProdutsByBrandAsync(string searchTerm);
}
