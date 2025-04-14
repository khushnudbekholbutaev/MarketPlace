using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Orders;

namespace TechStation.Service.Interfaces.Orders;

public interface IOrderService
{
    Task<int> CountAsync();
    Task<bool> RemoveAsync(long id);
    Task<OrderForResultDto> RetrieveByIdAsync(long id);
    Task<OrderForResultDto> AddAsync(OrderForCreationDto dto);
    Task<OrderForResultDto> ModifyAsync(long id, OrderForUpdateDto dto);
    Task<IEnumerable<OrderForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
