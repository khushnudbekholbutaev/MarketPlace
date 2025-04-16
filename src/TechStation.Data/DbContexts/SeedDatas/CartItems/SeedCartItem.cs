using Microsoft.EntityFrameworkCore;
using TechStation.Domain.Entities;

namespace TechStation.Data.DbContexts.SeedDatas.CartItems;

public static class SeedCartItem
{
    public static void SeedDataCartItem(ModelBuilder builder)
    {
        builder.Entity<CartItem>().HasData(
            new CartItem { Id = 1, UserId = 1, ProductId = 1, Quantity = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new CartItem { Id = 2, UserId = 2, ProductId = 2, Quantity = 2, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new CartItem { Id = 3, UserId = 3, ProductId = 3, Quantity = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new CartItem { Id = 4, UserId = 4, ProductId = 4, Quantity = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new CartItem { Id = 5, UserId = 5, ProductId = 5, Quantity = 1, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );
    }
}