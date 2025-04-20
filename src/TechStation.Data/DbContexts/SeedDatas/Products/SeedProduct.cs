using Microsoft.EntityFrameworkCore;
using TechStation.Domain.Entities;

namespace TechStation.Data.DbContexts.SeedDatas.Products;

public  class SeedProduct
{
    public static void SeedDataProduct(ModelBuilder builder)
    {
        builder.Entity<Product>().HasData(
            new Product { Id = 1, ProductName = "Dell XPS 13", Price = 999.99m, Description = "A powerful and sleek laptop for professionals.", CategoryId = 1, BrendId = 1, Images = "dell_xps_13.jpg", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Product { Id = 2, ProductName = "Logitech G Pro X", Price = 129.99m, Description = "A high-performance gaming mouse.", CategoryId = 2, BrendId = 2, Images = "logitech_g_pro_x.jpg", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Product { Id = 3, ProductName = "Corsair K95 RGB Platinum", Price = 199.99m, Description = "Mechanical gaming keyboard with RGB lighting.", CategoryId = 2, BrendId = 3, Images = "corsair_k95_rgb_platinum.jpg", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Product { Id = 4, ProductName = "HP Omen 15", Price = 1399.99m, Description = "A gaming laptop with top-tier specs.", CategoryId = 1, BrendId = 4, Images = "hp_omen_15.jpg", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new Product { Id = 5, ProductName = "Samsung Odyssey G7", Price = 749.99m, Description = "A curved gaming monitor with 240Hz refresh rate.", CategoryId = 4, BrendId = 5, Images = "samsung_odyssey_g7.jpg", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );
    }
}