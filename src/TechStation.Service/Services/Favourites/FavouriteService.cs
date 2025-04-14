using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechStation.Data.IRepositories;
using TechStation.Data.Repositories;
using TechStation.Domain.Configurations;
using TechStation.Domain.Entities;
using TechStation.Service.Commons.CollectionExtensions;
using TechStation.Service.DTOs.Favourites;
using TechStation.Service.Exceptions;
using TechStation.Service.Interfaces.Favourites;

namespace TechStation.Service.Services.Favourites;

public class FavouriteService : IFavouriteService
{
    private readonly IMapper mapper;
    private readonly IRepository<User> userRepository;
    public readonly IRepository<Product> productRepository;
    public readonly IRepository<Favourite> favouriteRepository;

    public FavouriteService(IMapper mapper,
        IRepository<User> userRepository,
        IRepository<Product> productRepository,
        IRepository<Favourite> favouriteRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.productRepository = productRepository;
        this.favouriteRepository = favouriteRepository;
    }

    public async Task<FavouriteForResultDto> AddAsync(FavouriteForCreationDto dto, bool token)
    {
        if (!token)
            return null;

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

        var mapped = mapper.Map<Favourite>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        await favouriteRepository.InsertAsync(mapped);

        return mapper.Map<FavouriteForResultDto>(mapped);
    }

    public async Task<int> CountAsync()
    {
        return await favouriteRepository.CountAsync();
    }

    public async Task<bool> RemoveAsync(long id, bool token)
    {
        // If token is false, return false without doing anything
        if (!token)
            return false;

        // Fetch the favourite record from the repository
        var favourite = await favouriteRepository.SelectAll()
            .Where(u => u.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        if (favourite is null)
            throw new TechStationException(404, "Favourite not found");

        // Proceed with the delete operation if token is true
        await favouriteRepository.DeleteAsync(id);

        return true;
    }

    public async Task<bool> ClearDataAsync(bool clear)
    {
        if (clear)
            return await favouriteRepository.ClearAsync();
        return false;
    }

    public async Task<ICollection<FavouriteForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var favouretes = await favouriteRepository.SelectAll()
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        return mapper.Map<ICollection<FavouriteForResultDto>>(favouretes);

    }
    public async Task<bool> ClearAllFavouritesAsync(bool clear = false)
    {
        if (!clear)
            return false;

        return await favouriteRepository.ClearAsync();
    }
    public async Task<FavouriteForResultDto> RetrieveByIdAsync(long id)
    {
        var favourite = await favouriteRepository.SelectAll()
            .Where(f => f.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (favourite is null)
            throw new TechStationException(404, "Favourite is not found");

        return mapper.Map<FavouriteForResultDto>(favourite);
    }
}
