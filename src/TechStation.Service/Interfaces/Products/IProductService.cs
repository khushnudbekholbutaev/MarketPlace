using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Products;

namespace TechStation.Service.Interfaces.Products;

public interface IProductService
{
    Task<int> CountAsync();
    Task<bool> RemoveAsync(long id);
    Task<ProductForResultDto> RetrieveByIdAsync(long id);
    Task<ProductForResultDto> AddAsync(ProductForCreationDto dto);
    Task<ProductForResultDto> ModifyAsync(long id,ProductForUpdateDto dto);
    Task<List<ProductForResultDto>> SearchByProductNameAsync(string searchTerm);
    Task<IEnumerable<ProductForResultDto>> RetrieveAllAsync(
        PaginationParams @params,
        decimal? minPrice = null,
        decimal? maxPrice = null,
        bool order = false, // True - kamayish, False - o‘sish
        string productName = null);


}
