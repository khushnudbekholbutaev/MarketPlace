using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using TechStation.Data.IRepositories;
using TechStation.Data.Repositories;
using TechStation.Domain.Configurations;
using TechStation.Domain.Entities;
using TechStation.Service.Commons.CollectionExtensions;
using TechStation.Service.DTOs.CartItems;
using TechStation.Service.Exceptions;
using TechStation.Service.Interfaces.CartItems;

namespace TechStation.Service.Services.CartItems;

public class CartItemService : ICartItemService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> userRepository;
    private readonly IRepository<Product> productRepository;
    private readonly IRepository<CartItem> cartItemRepository;

    public CartItemService(IRepository<Product> productRepository,
        IRepository<User> userRepository, 
        IRepository<CartItem> cartItemRepository, 
        IMapper mapper)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.productRepository = productRepository;
        this.cartItemRepository = cartItemRepository;
    }

    public async Task<CartItemForResultDto> AddAsync(CartItemForCreationDto dto, bool token, string operation = null)
    {
        if (!token)
            throw new TechStationException(403, "Unauthorized action");

        var user = await userRepository.SelectAll()
            .Where(u => u.Id == dto.UserId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (user is null)
            throw new TechStationException(404, "User is not found");

        var product = await productRepository.SelectAll()
            .Where(p => p.Id == dto.ProductId)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (product is null)
            throw new TechStationException(404, "Product is not found");

        var cartItem = await cartItemRepository.SelectAll()
            .Where(ci => ci.UserId == dto.UserId && ci.ProductId == dto.ProductId)
            .FirstOrDefaultAsync();

        if (cartItem is not null)
        {
            // Agar operation berilgan bo‘lsa, shunga qarab soni o‘zgartiramiz
            if (operation == "+")
            {
                cartItem.Quantity += dto.Quantity;
            }
            else if (operation == "-")
            {
                cartItem.Quantity -= dto.Quantity;
                if (cartItem.Quantity <= 0)
                {
                    await cartItemRepository.DeleteAsync(cartItem.Id);
                    return null; // Agar quantity 0 bo‘lsa, o‘chiramiz
                }
            }
            else if (operation is null)
            {
                // Agar operation berilmagan bo‘lsa, quantity ni o‘zgartirmaymiz
                cartItem.Quantity = dto.Quantity;
            }

            cartItem.UpdatedAt = DateTime.UtcNow;
            var updatedCartItem = await cartItemRepository.UpdateAsync(cartItem);
            return mapper.Map<CartItemForResultDto>(updatedCartItem);
        }

        // Agar mahsulot savatchada bo‘lmasa, yangi qo‘shamiz
        var mapped = mapper.Map<CartItem>(dto);
        mapped.CreatedAt = DateTime.UtcNow;

        var result = await cartItemRepository.InsertAsync(mapped);
        return mapper.Map<CartItemForResultDto>(result);
    }





    public async Task<int> CountAsync()
    {
        return await cartItemRepository.CountAsync();
    }
    public async Task<bool> ClearAllCartItemsAsync(bool clear = false)
    {
        if (!clear)
            return false;

        return await cartItemRepository.ClearAsync();
    }
    public async Task<CartItemForResultDto> ModifyAsync(long id, CartItemForUpdateDto dto)
    {
        var cartItem  = await cartItemRepository.SelectAll()
            .Where(ci => ci.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (cartItem is null)
            throw new TechStationException(404, "Cart Item is not found");
        var mapped = mapper.Map(dto,cartItem);
        mapped.UpdatedAt = DateTime.UtcNow;
        await cartItemRepository.UpdateAsync(mapped);

        return mapper.Map<CartItemForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id, bool token)
    {
        if (!token)
            return false; // If token is false, do not proceed with deletion

        var cartItem = await cartItemRepository.SelectAll()
            .Where(ci => ci.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (cartItem is null)
            throw new TechStationException(404, "Cart Item not found");

        await cartItemRepository.DeleteAsync(id);
        return true;
    }


    public async Task<ICollection<CartItemForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var carItems = await cartItemRepository.SelectAll()
            .Include(ci => ci.Product)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();
        return mapper.Map<ICollection<CartItemForResultDto>>(carItems);
    }

    public async Task<CartItemForResultDto> RetrieveByIdAsync(long id)
    {
        var cartItem = await cartItemRepository.SelectAll()
            .Where(ci => ci.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (cartItem is null)
            throw new TechStationException(404, "Cart Item is not found");

        return mapper.Map<CartItemForResultDto>(cartItem);
    }
}
