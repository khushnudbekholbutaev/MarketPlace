using Microsoft.EntityFrameworkCore;
using TechStation.Domain.Entities;

namespace TechStation.Data.DbContexts.SeedDatas.Users;

public  class SeedUser
{
    public static void SeedDataUser(ModelBuilder builder)
    {
        builder.Entity<User>().HasData(
            new User { Id = 1, FirstName = "John", LastName = "Doe", UserName = "johndoe", Email = "john@example.com", Password = "Password123", PhoneNumber = "123-456-7890", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new User { Id = 2, FirstName = "Jane", LastName = "Smith", UserName = "janesmith", Email = "jane@example.com", Password = "Password123", PhoneNumber = "123-456-7890", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new User { Id = 3, FirstName = "Alice", LastName = "Johnson", UserName = "alicejohnson", Email = "alice@example.com", Password = "Password123", PhoneNumber = "123-456-7890", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new User { Id = 4, FirstName = "Bob", LastName = "Brown", UserName = "bobbrown", Email = "bob@example.com", Password = "Password123", PhoneNumber = "123-456-7890", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new User { Id = 5, FirstName = "Charlie", LastName = "Davis", UserName = "charliedavis", Email = "charlie@example.com", Password = "Password123", PhoneNumber = "123-456-7890", CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );
    }
}