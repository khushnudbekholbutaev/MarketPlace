using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using TechStation.Data.IRepositories;
using TechStation.Domain.Configurations;
using TechStation.Domain.Entities;
using TechStation.Service.Commons.CollectionExtensions;
using TechStation.Service.DTOs.Brends;
using TechStation.Service.DTOs.Products;
using TechStation.Service.Exceptions;
using TechStation.Service.Interfaces.Brends;

namespace TechStation.Service.Services.Brends;

public class BrendService : IBrendService
{
    private readonly IMapper mapper;
    private readonly IRepository<Brend> brendRepository;

    public BrendService(IMapper mapper, IRepository<Brend> brendRepository)
    {
        this.mapper = mapper;
        this.brendRepository = brendRepository;
    }

    public async Task<BrendForResultDto> AddAsync(BrendForCreationDto dto)
    {
        var brend = await brendRepository.SelectAll()
            .Where(b => b.BrendName.ToLower() == dto.BrendName.ToLower())
            .FirstOrDefaultAsync();
        if (brend is not null)
            throw new TechStationException(409, "Brend is already exists");
        var mapped = mapper.Map<Brend>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        var result = await brendRepository.InsertAsync(mapped);

        return mapper.Map<BrendForResultDto>(result);
    }

    public async Task<BrendForResultDto> ModifyAsync(long id, BrendForUpdateDto dto)
    {
        var brend = await brendRepository.SelectAll()
            .Where(b => b.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (brend is null)
            throw new TechStationException(404, "Brend is not found");
        var mapped = mapper.Map(dto, brend);
        mapped.UpdatedAt = DateTime.UtcNow;
        await brendRepository.UpdateAsync(mapped);

        return mapper.Map<BrendForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var brend = await brendRepository.SelectAll()
            .Where(b => b.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (brend is null)
            throw new TechStationException(404, "Brend is not found");
        await brendRepository.DeleteAsync(id);

        return true;
    }

    public async Task<ICollection<BrendForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var brend = await brendRepository.SelectAll()
            .Include(b => b.Products)
            .ToPagedList(@params)
            .AsNoTracking()
            .ToListAsync();

        return mapper.Map<ICollection<BrendForResultDto>>(brend);
    }
    public async Task<ICollection<ProductForResultDto>> RetrieveAllProdutsByBrandAsync(string searchTerm)
    {
        var brand = await brendRepository.SelectAll()
            .Include(b => b.Products)
            .FirstOrDefaultAsync(p => p.BrendName == searchTerm);

        if (brand == null || brand.Products == null || !brand.Products.Any())
            return new List<ProductForResultDto>();

        return mapper.Map<ICollection<ProductForResultDto>>(brand.Products);
    }



    public async Task<int> CountAsync()
    {
        // Brendlar sonini olish
        return await brendRepository.CountAsync();
    }
    public async Task<BrendForResultDto> RetrieveByIdAsync(long id)
    {
        var brand = await brendRepository.SelectAll()
            .Where(b => b.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (brand is null)
            throw new TechStationException(404, "Brand is not found");

        return mapper.Map<BrendForResultDto>(brand);
    }

}
