using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.OrderDetails;

namespace TechStation.Service.Interfaces.OrderDetails;

public interface IOrderDetailService
{
    Task<int> CountAsync();
    Task<bool> RemoveAsync(long id);
    Task<OrderDetailForResultDto> RetrieveByIdAsync(long id);
    Task<OrderDetailForResultDto> AddAsync(OrderDetailForCreationDto dto);
    Task<OrderDetailForResultDto> ModifyAsync(long id, OrderDetailForUpdateDto dto);
    Task<IEnumerable<OrderDetailForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
