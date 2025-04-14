using TechStation.Domain.Configurations;
using TechStation.Service.DTOs.Payments;

namespace TechStation.Service.Interfaces.Payments;

public interface IPaymentService
{
    Task<int> CountAsync();
    public Task<bool> RemoveAsync(long id);
    public Task<PaymentForResultDto> RetrieveByIdAsync(long id);
    public Task<PaymentForResultDto> AddAsync(PaymentForCreationDto dto);
    public Task<PaymentForResultDto> ModifyAsync(long id, PaymentForUpdateDto dto);
    public Task<IEnumerable<PaymentForResultDto>> RetrieveAllAsync(PaginationParams @params);
}
