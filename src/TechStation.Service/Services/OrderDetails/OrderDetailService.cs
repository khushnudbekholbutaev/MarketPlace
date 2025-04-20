using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechStation.Data.IRepositories;
using TechStation.Domain.Configurations;
using TechStation.Domain.Entities;
using TechStation.Service.Commons.CollectionExtensions;
using TechStation.Service.DTOs.OrderDetails;
using TechStation.Service.Exceptions;
using TechStation.Service.Interfaces.OrderDetails;

namespace TechStation.Service.Services.OrderDetails;

public class OrderDetailService : IOrderDetailService
{
    private readonly IMapper mapper;
    private readonly IRepository<OrderDetail> orderDetailRepository;
    private readonly IRepository<Payment> paymentRepository;
    private readonly IRepository<Order> orderRepository;

    public OrderDetailService(IRepository<Order> orderRepository,
        IRepository<Payment> paymentRepository,
        IRepository<OrderDetail> orderDetailRepository, 
        IMapper mapper)
    {
        this.orderRepository = orderRepository;
        this.paymentRepository = paymentRepository;
        this.orderDetailRepository = orderDetailRepository;
        this.mapper = mapper;
    }

    public async Task<OrderDetailForResultDto> AddAsync(OrderDetailForCreationDto dto)
    {
        var order = await orderRepository.SelectAll()
            .Where(o => o.Id == dto.OrderId)
            .FirstOrDefaultAsync();
        if (order is null)
            throw new TechStationException(404, "Order is not null");
        var payment = await paymentRepository.SelectAll()
            .Where(p => p.Id == dto.PaymentId)
            .FirstOrDefaultAsync();
        if (payment is null)
            throw new TechStationException(404, "Payment is not null");
        var orderDetail = await orderDetailRepository.SelectAll()
            .Where(od => od.Quantity == dto.Quantity)
            .FirstOrDefaultAsync();
        if (orderDetail is not null)
            throw new TechStationException(409, "Order Detail is allready exists");


        var mapped = mapper.Map<OrderDetail>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        await orderDetailRepository.InsertAsync(mapped);

        return mapper.Map<OrderDetailForResultDto>(mapped);
    }

    public async Task<OrderDetailForResultDto> ModifyAsync(long id, OrderDetailForUpdateDto dto)
    {
        var order = await orderRepository.SelectAll()
            .Where(o => o.Id == dto.OrderId)
            .FirstOrDefaultAsync();
        if (order is null)
            throw new TechStationException(404, "Order is not null");
        var payment = await paymentRepository.SelectAll()
            .Where(p => p.Id == dto.PaymentId)
            .FirstOrDefaultAsync();
        if (payment is null)
            throw new TechStationException(404, "Payment is not null");
        var orderDetail = await orderDetailRepository.SelectAll()
            .Where(od => od.Quantity == dto.Quantity)
            .FirstOrDefaultAsync();
        if (orderDetail is not null)
            throw new TechStationException(409, "Order Detail is allready exists");

        var mapped = mapper.Map(dto, orderDetail);
        mapped.UpdatedAt = DateTime.UtcNow;
        await orderDetailRepository.UpdateAsync(mapped);

        return mapper.Map<OrderDetailForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var orderDetail = await orderDetailRepository.SelectAll()
            .Where(od => od.Id == id)
            .FirstOrDefaultAsync();
        if (orderDetail is null)
            throw new TechStationException(404, "Order Detail is not null");
        await orderDetailRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<OrderDetailForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var orderDetails = await orderDetailRepository.SelectAll()
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        return mapper.Map<IEnumerable<OrderDetailForResultDto>>(orderDetails);
    }
    public async Task<int> CountAsync()
    {
        return await orderDetailRepository.CountAsync();
    }
    public async Task<OrderDetailForResultDto> RetrieveByIdAsync(long id)
    {
        var orderDetail = await orderDetailRepository.SelectAll()
            .Where(od => od.Id == id)
            .FirstOrDefaultAsync();
        if (orderDetail is null)
            throw new TechStationException(404, "ORder detail is not found");

        return mapper.Map<OrderDetailForResultDto>(orderDetail);
    }
}
