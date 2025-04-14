using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TechStation.Data.IRepositories;
using TechStation.Domain.Configurations;
using TechStation.Domain.Entities;
using TechStation.Service.Commons.CollectionExtensions;
using TechStation.Service.DTOs.Categories;
using TechStation.Service.Exceptions;
using TechStation.Service.Interfaces.Categories;

namespace TechStation.Service.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly IMapper mapper;
    private readonly IRepository<Category> categoryRepository;
    private readonly IRepository<Catalog> catalogRepository;

    public CategoryService(IMapper mapper,
        IRepository<Category> categoryRepository,
        IRepository<Catalog> catalogRepository)
    {
        this.mapper = mapper;
        this.categoryRepository = categoryRepository;
        this.catalogRepository = catalogRepository;
    }

    public async Task<CategoryForResultDto> AddAsync(CategoryForCreationDto dto)
    {
        var catalog = await catalogRepository.SelectAll()
            .Where(ct => ct.Id == dto.CatalogId)
            .FirstOrDefaultAsync();
        if(catalog is null)
            throw new TechStationException(404,"Catalog is not found");
        var category = await categoryRepository.SelectAll()
            .Where(c => c.CategoryName == dto.CategoryName)
            .FirstOrDefaultAsync();
        if(category is not null)
            throw new TechStationException(409, "Category is already exists");

        var mapped = mapper.Map<Category>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        await categoryRepository.InsertAsync(mapped);

        return mapper.Map<CategoryForResultDto>(mapped);
    }

    public async Task<CategoryForResultDto> ModifyAsync(long id, CategoryForUpdateDto dto)
    {
        var catalog = await catalogRepository.SelectAll()
            .Where(c => c.Id == dto.CatalogId)
            .FirstOrDefaultAsync();
        if (catalog is null)
            throw new TechStationException(404, "Catalog is not found");
        var category = await categoryRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        if (category is null)
            throw new TechStationException(404, "Category is not found");

        var mapped = mapper.Map(dto, category);
        mapped.UpdatedAt = DateTime.UtcNow;
        await categoryRepository.UpdateAsync(mapped);

        return mapper.Map<CategoryForResultDto>(mapped);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var category = await catalogRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        if (category is null)
            throw new TechStationException(404, "Category is not found");

        await categoryRepository.DeleteAsync(id);
        return true;
    }

    public async Task<IEnumerable<CategoryForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var categories = await categoryRepository.SelectAll()
            .Include(c => c.Products)
            .ToPagedList(@params)
            .ToListAsync();

        return mapper.Map<IEnumerable<CategoryForResultDto>>(categories);
    }
    public async Task<int> CountAsync()
    {
        return await categoryRepository.CountAsync();
    }
    public async Task<CategoryForResultDto> RetrieveByIdAsync(long id)
    {
        var category = await categoryRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        if (category is null)
            throw new TechStationException(404, "Category is not found");

        return mapper.Map<CategoryForResultDto>(category);
    }
}
