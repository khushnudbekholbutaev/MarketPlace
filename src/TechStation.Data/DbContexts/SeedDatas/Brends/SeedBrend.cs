using Microsoft.EntityFrameworkCore;
using TechStation.Domain.Entities;

namespace TechStation.Data.DbContexts.SeedDatas.Brends;

public static class SeedBrend
{
    public static void SeedDataBrend(ModelBuilder builder)
    {
        builder.Entity<Brend>().HasData(
            new Brend { Id = 1, BrendName = "Dell", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Brend { Id = 2, BrendName = "Logitech", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Brend { Id = 3, BrendName = "Corsair", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Brend { Id = 4, BrendName = "HP", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Brend { Id = 5, BrendName = "Samsung", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );
    }
}
