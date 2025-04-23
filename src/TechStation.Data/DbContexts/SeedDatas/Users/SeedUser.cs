using Microsoft.EntityFrameworkCore;
using TechStation.Domain.Entities;
using TechStation.Domain.Enums;

namespace TechStation.Data.DbContexts.SeedDatas.Users;

public  class SeedUser
{
    public static void SeedDataUser(ModelBuilder builder)
    {
        builder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "Xurshid",
                LastName = "Oqmonov",
                UserName = "VanTux",
                Email = "xurshid@example.com",
                Password = "Password123",
                PhoneNumber = "+998935000000",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Role = Role.admin
            },
            new User
            {
                Id = 2,
                FirstName = "Abdulbosit",
                LastName = "Abdullayev",
                UserName = "abdulbosit",
                Email = "abdulbosit@example.com",
                Password = "Password123",
                PhoneNumber = "+998901234567",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Role = Role.superAdmin
            },
            new User
            {
                Id = 3,
                FirstName = "Dilshod",
                LastName = "Karimov",
                UserName = "dilshodk",
                Email = "dilshod@example.com",
                Password = "Password123",
                PhoneNumber = "+998931112233",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Role = Role.user
            },
            new User
            {
                Id = 4,
                FirstName = "Nigora",
                LastName = "Yusupova",
                UserName = "nigora",
                Email = "nigora@example.com",
                Password = "Password123",
                PhoneNumber = "+998947654321",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Role = Role.user
            },
            new User
            {
                Id = 5,
                FirstName = "Javohir",
                LastName = "Toirov",
                UserName = "javohirt",
                Email = "javohir@example.com",
                Password = "Password123",
                PhoneNumber = "+998933334455",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Role = Role.user
            }
        );
    }
}
