using Microsoft.EntityFrameworkCore;
using TechStation.Domain.Entities;

namespace TechStation.Data.DbContexts.SeedDatas.Orders;

public  class SeedOrder
{
    public static void SeedDataOrder(ModelBuilder builder)
    {
        builder.Entity<Order>().HasData(
            new Order { Id = 1, UserId = 1, ProductId = 1, TotalAmount = 999.99m, Quantity = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Order { Id = 2, UserId = 2, ProductId = 2, TotalAmount = 259.98m, Quantity = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Order { Id = 3, UserId = 3, ProductId = 3, TotalAmount = 199.99m, Quantity = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Order { Id = 4, UserId = 4, ProductId = 4, TotalAmount = 1399.99m, Quantity = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Order { Id = 5, UserId = 5, ProductId = 5, TotalAmount = 749.99m, Quantity = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );
    }
}