using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechStation.Data.IRepositories;
using TechStation.Domain.Configurations;
using TechStation.Domain.Entities;
using TechStation.Service.Commons.CollectionExtensions;
using TechStation.Service.DTOs.Orders;
using TechStation.Service.Exceptions;
using TechStation.Service.Interfaces.Orders;

namespace TechStation.Service.Services.Orders;

public class OrderService : IOrderService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> userRepository;
    private readonly IRepository<Order> orderRepository;
    private readonly IRepository<Product> productRepository;

    public OrderService(IMapper mapper, 
        IRepository<User> userRepository, 
        IRepository<Order> orderRepository,
        IRepository<Product> productRepository)
    {
        this.mapper = mapper;
        this.orderRepository = orderRepository;
        this.userRepository = userRepository;
        this.productRepository = productRepository;
    }

    public async Task<OrderForResultDto> AddAsync(OrderForCreationDto dto)
    {
        var user = await userRepository.SelectAll()
            .Where(u => u.Id == dto.UserId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new TechStationException(404, "User is not found");
        var product = await productRepository.SelectAll()
            .Where(p => p.Id == dto.ProductId)
            .FirstOrDefaultAsync();
        if (product is null)
            throw new TechStationException(404, "Product is not found");
        var order = await orderRepository.SelectAll()
            .Where(o => o.Quantity == dto.Quantity)
            .FirstOrDefaultAsync();
        if(order is not null)
            throw new TechStationException(409, "Order is already exist");

        var totalAmount = product.Price * dto.Quantity;

        var mapped = mapper.Map<Order>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.TotalAmount = totalAmount;
        await orderRepository.InsertAsync(mapped);

        return mapper.Map<OrderForResultDto>(mapped);
    }

    public async Task<OrderForResultDto> ModifyAsync(long id, OrderForUpdateDto dto)
    {
        var user = await userRepository.SelectAll()
            .Where(u => u.Id == dto.UserId)
            .FirstOrDefaultAsync();
        if (user is null)
            throw new TechStationException(404, "User is not found");
        var product = await productRepository.SelectAll()
            .Where(p => p.Id == dto.ProductId)
            .FirstOrDefaultAsync();
        if (product is null)
            throw new TechStationException(404, "Product is not found");
        var order = await orderRepository.SelectAll()
            .Where(o => o.Quantity == dto.Quantity)
            .FirstOrDefaultAsync();
        if (order is not null)
            throw new TechStationException(409, "Order is already exist");

        var totalAmount = product.Price * dto.Quantity;


        var mapped = mapper.Map<Order>(dto);
        mapped.UpdatedAt = DateTime.UtcNow;
        mapped.TotalAmount = totalAmount;
        await orderRepository.UpdateAsync(mapped);

        return mapper.Map<OrderForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var order = await orderRepository.SelectAll()
            .Where(o => o.Id == id)
            .FirstOrDefaultAsync();
        if (order is null)
            throw new TechStationException(404, "Order is not found");
        await orderRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<OrderForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var orders = await orderRepository.SelectAll()
            .Include(o => o.OrderDetails)
            .ToPagedList(@params)
            .ToListAsync();

        return mapper.Map<IEnumerable<OrderForResultDto>>(orders);
    }
    public async Task<int> CountAsync()
    {
        return await orderRepository.CountAsync();
    }
    public async Task<OrderForResultDto> RetrieveByIdAsync(long id)
    {
        var order =  await orderRepository.SelectAll()
            .Where(o => o.Id == id)
            .FirstOrDefaultAsync();
        if (order is null)
            throw new TechStationException(404, "Order is not found");

        return mapper.Map<OrderForResultDto>(order);
    }
}
