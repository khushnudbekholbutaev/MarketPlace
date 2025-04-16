using Microsoft.EntityFrameworkCore;
using TechStation.Domain.Entities;

namespace TechStation.Data.DbContexts.SeedDatas.Categories;

public static class SeedCategory
{
    public static void SeedDataCategory(ModelBuilder builder)
    {
        builder.Entity<Category>().HasData(
            new Category { Id = 1, CategoryName = "Laptops", Description = "Portable computers for work and play", CatalogId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { Id = 2, CategoryName = "Accessories", Description = "Computer accessories including mice, keyboards, and more", CatalogId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { Id = 3, CategoryName = "Gaming", Description = "Gaming equipment including chairs, desks, and peripherals", CatalogId = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { Id = 4, CategoryName = "Monitors", Description = "High-quality monitors for work and entertainment", CatalogId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Category { Id = 5, CategoryName = "Desktops", Description = "Powerful desktop computers for gaming and work", CatalogId = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );
    }
}