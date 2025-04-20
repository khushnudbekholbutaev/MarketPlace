using Microsoft.EntityFrameworkCore;
using TechStation.Domain.Entities;
using TechStation.Domain.Enums;

namespace TechStation.Data.DbContexts.SeedDatas.Payments;

public  class SeedPayment
{
    public static void SeedDataPayment(ModelBuilder builder)
    {
        builder.Entity<Payment>().HasData(
            new Payment
            {
                Id = 1,
                PaymentMethod = "Credit Card",
                Amount = 999.99m,
                Status = Status.Paid,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Payment
            {
                Id = 2,
                PaymentMethod = "PayPal",
                Amount = 259.98m,
                Status = Status.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Payment
            {
                Id = 3,
                PaymentMethod = "Credit Card",
                Amount = 199.99m,
                Status = Status.Paid,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Payment
            {
                Id = 4,
                PaymentMethod = "Credit Card",
                Amount = 1399.99m,
                Status = Status.Paid,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Payment
            {
                Id = 5,
                PaymentMethod = "PayPal",
                Amount = 749.99m,
                Status = Status.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}
