using AutoMapper;
using Azure;
using FuzzySharp;
using Microsoft.EntityFrameworkCore;
using TechStation.Data.IRepositories;
using TechStation.Domain.Configurations;
using TechStation.Domain.Entities;
using TechStation.Service.Commons.CollectionExtensions;
using TechStation.Service.DTOs.Banners;
using TechStation.Service.DTOs.Brends;
using TechStation.Service.Exceptions;
using TechStation.Service.Helpers;
using TechStation.Service.Interfaces.Banners;

namespace TechStation.Service.Services.Banners;

public class BannerService : IBannerService
{
    private readonly IMapper mapper;
    private readonly IRepository<Banner> bannerRepository;
    private readonly IRepository<Brend> brendRepository;
    private readonly IRepository<Category> categoryRepository;

    public BannerService(IMapper mapper, 
        IRepository<Banner> bannerRepository,
        IRepository<Brend> brendRepository, 
        IRepository<Category> categoryRepository)
    {
        this.mapper = mapper;
        this.bannerRepository = bannerRepository;
        this.brendRepository = brendRepository;
        this.categoryRepository = categoryRepository;
    }

    public async Task<BannerForResultDto> AddAsync(BannerForCreationDto dto)
    {
        // Rasmlar sonini tekshirish
        if (dto.Images == null || !dto.Images.Any())
            throw new TechStationException(400, "At least 4 images are required.");

        if (dto.Images.Count() < 4 || dto.Images.Count() > 10)
            throw new TechStationException(400, "You must upload between 4 and 10 images.");
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
        //var banner = await bannerRepository.SelectAll()
        //    .Where(b => b.NameUz.ToLower() == dto.NameUz.ToLower())
        //    .FirstOrDefaultAsync();
        //if (banner is not null)
        //    throw new TechStationException(409,"Banner is already exists");
        #region Images
        // Rasmlar yo'llari uchun ro'yxat
        var imageResults = new List<string>();

        foreach (var image in dto.Images)
        {
            var imageFileName = Guid.NewGuid().ToString("N") + Path.GetExtension(image.FileName);
            var imageRootPath = Path.Combine(WebEnvironmentHost.WebRootPath, "Assets", "BannerAssets", "Images", imageFileName);

            using (var stream = new FileStream(imageRootPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
                await stream.FlushAsync();
            }

            string imagePath = Path.Combine("Assets", "BannerAssets", "Images", imageFileName);
            imageResults.Add(imagePath);
        }

        // Rasmlar yo'llarini birlashtirish
        var imagesString = string.Join(";", imageResults);
        #endregion

        var mapped = mapper.Map<Banner>(dto);
        mapped.CreatedAt = DateTime.UtcNow;
        mapped.Images = imagesString;
        await bannerRepository.InsertAsync(mapped);

        return mapper.Map<BannerForResultDto>(mapped);
    }

    public async Task<BannerForResultDto> ModifyAsync(long id, BannerForUpdateDto dto)
    {
        var banner = await bannerRepository.SelectAll()
            .Where(b => b.Id == id)
            .FirstOrDefaultAsync();
        if (banner is  null)
            throw new TechStationException(409, "Banner is not found");
        #region Image
        // Mavjud rasmlar ro'yxatini olish
        var existingImages = banner.Images?.Split(';') ?? Array.Empty<string>();

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
                var imageRootPath = Path.Combine(WebEnvironmentHost.WebRootPath, "Assets", "BannerAssets", "Images", imageFileName);

                using (var stream = new FileStream(imageRootPath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                    await stream.FlushAsync();
                }

                var imagePath = Path.Combine("Assets", "BannerAssets", "Images", imageFileName);
                newImagePaths.Add(imagePath);
            }
        }

        // Yangi rasmlarni birlashtirish
        var imagesString = string.Join(";", newImagePaths);
        #endregion
        var mapped = mapper.Map(dto,banner);
        mapped.UpdatedAt = DateTime.UtcNow;
        mapped.Images = imagesString;
        await bannerRepository.UpdateAsync(mapped);

        return mapper.Map<BannerForResultDto>(dto);
    }

    public async Task<bool> RemoveAsync(long id)
    {
        var banner = await bannerRepository.SelectAll()
            .Where(b => b.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (banner is null)
            throw new TechStationException(404, "Banner is not found");
        #region Image
        var imageFullPath = Path.Combine(WebEnvironmentHost.WebRootPath, banner.Images);

        if (File.Exists(imageFullPath))
            File.Delete(imageFullPath);
        #endregion
        await bannerRepository.DeleteAsync(id);

        return true;
    }

    public async Task<ICollection<BannerForResultDto>> RetrieveAllAsync(PaginationParams @params)
    {
        var banners = await bannerRepository.SelectAll()
            .ToPagedList(@params)
            .ToListAsync();

        return mapper.Map<ICollection<BannerForResultDto>>(banners);
    }



    public async Task<int> CountAsync()
    {
        return await bannerRepository.CountAsync();
    }
    public async Task<BannerForResultDto> RetrieveByIdAsync(long id)
    {
        var banner = await bannerRepository.SelectAll()
            .Where(b => b.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        if (banner is null)
            throw new TechStationException(404, "Banner is not found");

        return mapper.Map<BannerForResultDto>(banner);
    }

    public Task<BannerForResultDto> GetBannerName(string name)
    {
        throw new NotImplementedException();
    }
}
