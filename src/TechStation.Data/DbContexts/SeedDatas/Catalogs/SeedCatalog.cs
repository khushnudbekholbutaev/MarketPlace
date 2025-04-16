using Microsoft.EntityFrameworkCore;
using TechStation.Domain.Entities;

namespace TechStation.Data.DbContexts.SeedDatas.Catalogs;

public static class SeedCatalog
{
    public static void SeedDataCatalog(ModelBuilder builder)
    {
        builder.Entity<Catalog>().HasData(
            new Catalog { Id = 1, Name = "Computers & Laptops", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Catalog { Id = 2, Name = "Gaming & Peripherals", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Catalog { Id = 3, Name = "Monitors & Displays", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );
    }
}