using Microsoft.EntityFrameworkCore;
using TechStation.Domain.Entities;

namespace TechStation.Data.DbContexts.SeedDatas.Banners;

public class SeedBanner
{
    public static void SeedDataBanner(ModelBuilder builder)
    {
        builder.Entity<Banner>().HasData(
            new Banner { Id = 1, Images = "banner_computers.jpg", BrendId = 1, CategoryId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Banner { Id = 2, Images = "banner_accessories.jpg", BrendId = 2, CategoryId = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Banner { Id = 3, Images = "banner_gaming.jpg", BrendId = 3, CategoryId = 3, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Banner { Id = 4, Images = "banner_laptops.jpg", BrendId = 4, CategoryId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Banner { Id = 5, Images = "banner_monitors.jpg", BrendId = 5, CategoryId = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );
    }
}
