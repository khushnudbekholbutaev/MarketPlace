using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using TechStation.Data.DbContexts.SeedDatas.Banners;
using TechStation.Data.DbContexts.SeedDatas.Brends;
using TechStation.Data.DbContexts.SeedDatas.CartItems;
using TechStation.Data.DbContexts.SeedDatas.Catalogs;
using TechStation.Data.DbContexts.SeedDatas.Categories;
using TechStation.Data.DbContexts.SeedDatas.Orders;
using TechStation.Data.DbContexts.SeedDatas.Payments;
using TechStation.Data.DbContexts.SeedDatas.Products;
using TechStation.Data.DbContexts.SeedDatas.Users;
using TechStation.Domain.Entities;

namespace TechStation.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<Brend> Brends { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Favourite> Favorites { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder
    //        .ConfigureWarnings(warnings =>
    //            warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
    //}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // OrderDetail bog‘lanishlari
        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Order)
            .WithMany(o => o.OrderDetails)
            .HasForeignKey(od => od.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderDetail>()
            .HasOne(od => od.Payment)
            .WithMany(p => p.OrderDetails)
            .HasForeignKey(od => od.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Order bog‘lanishlari
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Product)
            .WithMany()
            .HasForeignKey(o => o.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Product bog‘lanishlari
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Brend)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrendId)
            .OnDelete(DeleteBehavior.Cascade);

        // Favourite bog‘lanishlari
        modelBuilder.Entity<Favourite>()
            .HasOne(f => f.User)
            .WithMany(u => u.Favourites)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Favourite>()
            .HasOne(f => f.Product)
            .WithMany()
            .HasForeignKey(f => f.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // CartItem bog‘lanishlari
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.User)
            .WithMany(u => u.CartItems)
            .HasForeignKey(ci => ci.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Product)
            .WithMany()
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        // Banner bog‘lanishlari
        modelBuilder.Entity<Banner>()
            .HasOne(b => b.Brend)
            .WithMany()
            .HasForeignKey(b => b.BrendId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Banner>()
            .HasOne(b => b.Category)
            .WithMany()
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // Catalog va Category bog‘lanishlari
        modelBuilder.Entity<Category>()
            .HasOne(c => c.Catalog)
            .WithMany(cat => cat.Categories)
            .HasForeignKey(c => c.CatalogId)
            .OnDelete(DeleteBehavior.Cascade);

        //// UserRole with USe
        //modelBuilder.Entity<UserRole>()
        //    .HasOne(ur => ur.User)
        //    .WithOne(u => u.UserRole)
        //    .HasForeignKey<UserRole>(ur => ur.UserId)
        //    .OnDelete(DeleteBehavior.Cascade);

        // Decimal tiplarini aniqlashtirish
        modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasPrecision(18, 2);
        modelBuilder.Entity<OrderDetail>().Property(od => od.TotalAmount).HasPrecision(18, 2);
        modelBuilder.Entity<Payment>().Property(p => p.Amount).HasPrecision(18, 2);
        modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
        
        SeedData(modelBuilder);
    }


    private static void SeedData(ModelBuilder modelBuilder)
    {
        SeedBanner.SeedDataBanner(modelBuilder);
        SeedBrend.SeedDataBrend(modelBuilder);
        SeedCartItem.SeedDataCartItem(modelBuilder);
        SeedCatalog.SeedDataCatalog(modelBuilder);
        SeedCategory.SeedDataCategory(modelBuilder);
        SeedOrder.SeedDataOrder(modelBuilder);
        SeedPayment.SeedDataPayment(modelBuilder);
        SeedProduct.SeedDataProduct(modelBuilder);
        SeedUser.SeedDataUser(modelBuilder);
        SeedUserRole.SeedDataUserRole(modelBuilder);
    } 
}
