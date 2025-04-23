using Microsoft.EntityFrameworkCore;
using TechStation.Domain.Entities;
using TechStation.Domain.Enums;

namespace TechStation.Data.DbContexts.SeedDatas.Users;

public  class SeedUserRole
{
    public static void SeedDataUserRole(ModelBuilder builder)
    {
        builder.Entity<UserRole>().HasData(
            new UserRole { Id = 1, UserId = 1, Role = Role.admin, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new UserRole { Id = 2, UserId = 2, Role = Role.user, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new UserRole { Id = 3, UserId = 3, Role = Role.user, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new UserRole { Id = 4, UserId = 4, Role = Role.superAdmin, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow },
            new UserRole { Id = 5, UserId = 5, Role = Role.user, CreatedAt = DateTime.UtcNow, UpdatedAt = DateTime.UtcNow }
        );
    }
}

