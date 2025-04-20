using AutoMapper;
using FuzzySharp;
using Microsoft.EntityFrameworkCore;
using TechStation.Data.DbContexts;
using TechStation.Data.IRepositories;
using TechStation.Domain.Configurations;
using TechStation.Domain.Entities;
using TechStation.Domain.Enums;
using TechStation.Service.DTOs.Products;
using TechStation.Service.Exceptions;
using TechStation.Service.Helpers;
using TechStation.Service.Interfaces.Files;
using TechStation.Service.Interfaces.Products;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TechStation.Service.Services.Products;

public class ProductService : IProductService
{
    private readonly IMapper mapper;
    private readonly IRepository<Category> categoryRepository;
    private readonly IRepository<Product> productRepository;
    private readonly IFileUploadService fileUploadService;
    private readonly IRepository<Brend> brendRepository;
    private readonly AppDbContext appDbContext;
    public ProductService(IMapper mapper,
        IRepository<Category> categoryRepository,
        IRepository<Product> productRepository,
        IFileUploadService fileUploadService,
        IRepository<Brend> brendRepository,
        AppDbContext appDbContext)
    {
        this.mapper = mapper;
        this.categoryRepository = categoryRepository;
        this.productRepository = productRepository;
        this.fileUploadService = fileUploadService;
        this.brendRepository = brendRepository;
        this.appDbContext = appDbContext;
    }
    public IQueryable<Product> SelectAll()
    {
        // OnModelCreating da belgilangan saralashni qo'llash
        return appDbContext.Products
            .OrderBy(p => p.Price); // Price bo'yicha o'sish tartibida saralash
    }
    public async Task<ProductForResultDto> AddAsync(ProductForCreationDto dto)
    {
        // Rasmlar sonini tekshirish
        if (dto.Images == null || !dto.Images.Any())
            throw new TechStationException(400, "At least 3 images are required.");

        if (dto.Images.Count() < 3 || dto.Images.Count() > 10)
            throw new TechStationException(400, "You must upload between 3 and 10 images.");

        var category = await categoryRepository.SelectAll()
            .Where(c => c.Id == dto.CategoryId)
            .FirstOrDefaultAsync();
        if (category is null)
            throw new TechStationException(404, "Category is not found");

        var brend = await brendRepository.SelectAll()
            .Where(b => b.Id == dto.BrendId)
            .FirstOrDefaultAsync();
        if (brend is null)
            throw new TechStationException(404, "Brend is not found");

        var product = await productRepository.SelectAll()
            .Where(p => p.ProductName.ToLower() == dto.ProductName.ToLower())
            .FirstOrDefaultAsync();
        if (product is not null)
            throw new TechStationException(409, "Product already exists");

        #region Images
        // Rasmlar yo'llari uchun ro'yxat
        var imageResults = new List<string>();

        foreach (var image in dto.Images)
        {
            var imageFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
            var imageRootPath = Path.Combine(WebEnvironmentHost.WebRootPath, "Assets", "ProductAssets", "Images", imageFileName);

            using (var stream = new FileStream(imageRootPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
                await stream.FlushAsync();
            }

            string imagePath = Path.Combine("Assets", "ProductAssets", "Images", imageFileName);
            imageResults.Add(imagePath);
        }

        // Rasmlar yo'llarini birlashtirish
        var imagesString = string.Join(";", imageResults);
        #endregion

        // Ma'lumotlarni to'ldirish
        var mapped = mapper.Map<Product>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.Images = imagesString;

        await productRepository.InsertAsync(mapped);

        return mapper.Map<ProductForResultDto>(mapped);
    }

    public async Task<ProductForResultDto> ModifyAsync(long id, ProductForUpdateDto dto)
    {
        var category = await categoryRepository.SelectAll()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
        if (category is null)
            throw new TechStationException(404, "Category is not found");

        var brend = await brendRepository.SelectAll()
            .Where (b => b.Id == id)
            .FirstOrDefaultAsync();
        if (brend is null)
            throw new TechStationException(404, "Brend is not found");
            
        var product = await productRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        if (product is null)
            throw new TechStationException(404, "Product is not found");

        #region Image
        // Mavjud rasmlar ro'yxatini olish
        var existingImages = product.Images?.Split(';') ?? Array.Empty<string>();

        // Mavjud rasmlarni o'chirish
        foreach (var image in existingImages)
        {
            var imageFullPath = Path.Combine(WebEnvironmentHost.WebRootPath, image);
            if (File.Exists(imageFullPath))
                File.Delete(imageFullPath);
        }

        // Yangi rasmlar ro'yxatini saqlash
        var newImagePaths = new List<string>();

        if (dto.Images != null && dto.Images.Any())
        {
            foreach (var image in dto.Images)
            {
                var imageFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
                var imageRootPath = Path.Combine(WebEnvironmentHost.WebRootPath, "Assets", "ProductAssets", "Images", imageFileName);

                using (var stream = new FileStream(imageRootPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                    await stream.FlushAsync();
                }

                var imagePath = Path.Combine("Assets", "ProductAssets", "Images", imageFileName);
                newImagePaths.Add(imagePath);
            }
        }

        // Yangi rasmlarni birlashtirish
        var imagesString = string.Join(";", newImagePaths);
        #endregion

        // Ob'ektni yangilash
        var mapped = mapper.Map(dto, product);
        mapped.UpdatedAt = DateTime.UtcNow;
        mapped.Images = imagesString;

        await productRepository.UpdateAsync(mapped);

        return mapper.Map<ProductForResultDto>(mapped);
    }


    public async Task<bool> RemoveAsync(long id)
    {
        var product = await productRepository.SelectAll()
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
        if (product is null)
            throw new TechStationException(404, "Product is not found");
        #region Image
        var imageFullPath = Path.Combine(WebEnvironmentHost.WebRootPath, product.Images);

        if (File.Exists(imageFullPath))
            File.Delete(imageFullPath);
        #endregion
        await productRepository.DeleteAsync(id);

        return true;
    }
    public async Task<IEnumerable<ProductForResultDto>> RetrieveAllAsync(
    PaginationParams @params,
    decimal? minPrice = null,
    decimal? maxPrice = null,
    bool order = false, // True - oshish tartibi, False - kamayish tartibi
    string productName = null)
    {
        var query = productRepository.SelectAll();

        if (query == null)
        {
            throw new Exception("Ma'lumotlar bazasidan ma'lumot olishda xatolik yuz berdi. Query null qiymatga ega.");
        }

        // Filter va sort qoidalarini qo'llash
        var filteredList = ApplyFiltersAndSorting(query, minPrice, maxPrice, productName, order);

        // Alohida listga olish
        var sortedList = order ?
            filteredList.OrderBy(p => p.Price).ToList() :
            filteredList.OrderByDescending(p => p.Price).ToList();

        // Agar list bo'sh bo'lsa, bo'sh qaytarish
        if (!sortedList.Any())
        {
            return Enumerable.Empty<ProductForResultDto>();
        }

        // DTO'ga mapping va natijani qaytarish
        return sortedList.Select(p => new ProductForResultDto
        {
            Id = p.Id,
            ProductName = p.ProductName,
            Price = p.Price,
            Description = p.Description,
            CategoryId = p.CategoryId,
            Images = p.Images,
            BrendId = p.BrendId,
        });
    }

    private IQueryable<Product> ApplyFiltersAndSorting(
     IQueryable<Product> query,
     decimal? minPrice,
     decimal? maxPrice,
     string productName,
     bool order)
    {
        if (query == null)
        {
            throw new ArgumentNullException(nameof(query), "Query null qiymatga ega bo'lishi mumkin emas.");
        }

        if (minPrice.HasValue)
        {
            query = query.Where(p => p.Price >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= maxPrice.Value);
        }

        if (!string.IsNullOrEmpty(productName))
        {
            string search = $"%{productName.ToLower()}%";
            query = query.Where(x => EF.Functions.Like(x.ProductName.ToLower(), search));
        }

        if (order)
        {
            query = query.OrderBy(p => p.Price);
        }
        else
        {
            query = query.OrderByDescending(p => p.Price);
        }

        return query;
    }



    public async Task<int> CountAsync()
    {
        return await productRepository.CountAsync();
    }
    public async Task<ProductForResultDto> RetrieveByIdAsync(long id)
    {
        var product = await productRepository.SelectAll()
            .Where(product => product.Id == id)
            .FirstOrDefaultAsync();
        if (product is null)
            throw new TechStationException(404, "Product is not found");

        return mapper.Map<ProductForResultDto>(product);
    }

    public async Task<List<ProductForResultDto>> SearchByProductNameAsync(string searchTerm)
    {
        var products = await productRepository.SelectAll().ToListAsync();

        var fuzzyResults = products
                  .Select(product => new
                  {
                      Product = product,
                      Score = Fuzz.PartialRatio(product.ProductName.ToLower(), searchTerm.ToLower())  
                  })
                  .Where(result => result.Score >= 80)
                  .OrderByDescending(result => result.Score)
                  .ToList();

        return mapper.Map<List<ProductForResultDto>>(fuzzyResults.Select(p => p.Product).ToList());
    }

    public async Task<List<ProductForResultDto>> SortByPriceAsync(long price, SortPrice sort)
    {
        var query = appDbContext.Products.AsQueryable();

        if (sort == SortPrice.ascending)
        {
            query = query
                .Where(p => p.Price >= price)
                .OrderBy(p => p.Price);
        }
        else if (sort == SortPrice.descending)
        {
            if (price != 0 && price != null)
                query = query.Where(p => p.Price <= price);

            query = query.OrderByDescending(p => p.Price);
        }

        var products = await query.ToListAsync();

        return mapper.Map<List<ProductForResultDto>>(products);
    }



}
