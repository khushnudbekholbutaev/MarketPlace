using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechStation.Data.IRepositories;
using TechStation.Domain.Configurations;
using TechStation.Domain.Entities;
using TechStation.Service.Commons.CollectionExtensions;
using TechStation.Service.DTOs.Catalogs;
using TechStation.Service.Exceptions;
using TechStation.Service.Interfaces.Catalogs;

namespace TechStation.Service.Services.Catalogs;

public class CatalogService : ICatalogService
{
    private readonly IMapper mapper;
    private readonly IRepository<Catalog> catalogRepository;
    public CatalogService(IMapper mapper, IRepository<Catalog> catalogRepository)
    {
        this.mapper = mapper;
        this.catalogRepository = catalogRepository;
    }

    public async  Task<CatalogForResultDto> AddAsync(CatalogForCreationDto dto)
    {
        var catalog = await catalogRepository.SelectAll()
            .Where(c => c.Name == dto.Name)
            .FirstOrDefaultAsync();
        if(catalog is not null)
            throw new TechStationException(409, "Catalog is already exists");

        var mapped = mapper.Map<Catalog>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        await catalogRepository.InsertAsync(mapped);

        return mapper.Map<CatalogForResultDto>(mapped);
    }

    public async Task<CatalogForResultDto> ModifyAsync(long id, CatalogForUpdateDto dto)
    {
        var catalog = await catalogRepository.SelectAll()
            .Where(c => c.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if(catalog is  null)
            throw new TechStationException(404, "Catalog is not found");

        var mapped = mapper.Map(dto, catalog);
        mapped.UpdatedAt = DateTime.UtcNow;
        await catalogRepository.UpdateAsync(mapped);

        return mapper.Map<CatalogForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var catalog = await catalogRepository.SelectAll()
            .Where(c => c.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (catalog is null)
            throw new TechStationException(404, "Catalog is not found");

        await catalogRepository.DeleteAsync(id);

        return true;
    }

    public async Task<IEnumerable<CatalogForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var catalogs = await catalogRepository.SelectAll()
            .Include(c=>c.Categories)
            .ToPagedList(@params)
            .ToListAsync();

        return mapper.Map<IEnumerable<CatalogForResultDto>>(catalogs);
    }
    public async Task<int> CountAsync()
    {
        return await catalogRepository.CountAsync();
    }
    public async Task<CatalogForResultDto> RetrieveByIdAsync(long id)
    {
        var catalog = await catalogRepository.SelectAll()
            .Where(c => c.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (catalog is null)
            throw new TechStationException(404, "Catalog is not found");

        return mapper.Map<CatalogForResultDto>(catalog);
    }
}
