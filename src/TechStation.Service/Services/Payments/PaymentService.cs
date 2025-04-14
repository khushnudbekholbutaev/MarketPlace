using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechStation.Data.IRepositories;
using TechStation.Domain.Configurations;
using TechStation.Domain.Entities;
using TechStation.Service.Commons.CollectionExtensions;
using TechStation.Service.DTOs.Payments;
using TechStation.Service.Exceptions;
using TechStation.Service.Interfaces.Payments;

namespace TechStation.Service.Services.Payments;

public class PaymentService : IPaymentService
{
    private readonly IMapper mapper;
    private readonly IRepository<Payment> paymentRepository;

    public PaymentService(IMapper mapper, IRepository<Payment> paymentRepository)
    {
        this.mapper = mapper;
        this.paymentRepository = paymentRepository;
    }

    public async Task<PaymentForResultDto> AddAsync(PaymentForCreationDto dto)
    {
        var payment = await paymentRepository.SelectAll()
            .Where(p => p.Amount == dto.Amount)
            .FirstOrDefaultAsync();
        if(payment is not null)
            throw new TechStationException(409, "Payment already exists");
        var mapped = mapper.Map<Payment>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        await paymentRepository.InsertAsync(mapped);

        return mapper.Map<PaymentForResultDto>(mapped);
    }

    public async Task<PaymentForResultDto> ModifyAsync(long id, PaymentForUpdateDto dto)
    {
        var payment = await paymentRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        if(payment is null)
            throw new TechStationException(404, "Payment not found");
        var mapped = mapper.Map(dto, payment);
        mapped.UpdatedAt = DateTime.UtcNow;
        await paymentRepository.UpdateAsync(mapped);

        return mapper.Map<PaymentForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var payment = await paymentRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        if (payment is null)
            throw new TechStationException(404, "Payment not found");
        await paymentRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<PaymentForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var payments = await paymentRepository.SelectAll()
            .Include(p => p.OrderDetails)
            .ToPagedList(@params)
            .ToListAsync();

        return mapper.Map<IEnumerable<PaymentForResultDto>>(payments);
    }
    public async Task<int> CountAsync()
    {
        return await paymentRepository.CountAsync(); 
    }
    public async Task<PaymentForResultDto> RetrieveByIdAsync(long id)
    {
        var payment = await paymentRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        if (payment is null)
            throw new TechStationException(404, "Payment is not found");

        return mapper.Map<PaymentForResultDto>(payment);
    }
}
