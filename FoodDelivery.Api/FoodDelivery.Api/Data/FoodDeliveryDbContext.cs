using FoodDelivery.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Api.Data;

/// <summary>
/// Provides the Entity Framework Core database context for the food delivery order management system.
/// </summary>
public class FoodDeliveryDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FoodDeliveryDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Gets or sets the collection of orders in the database.
    /// </summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>
    /// Configures the model and seeds initial data for the application.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure the entity model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.CustomerName).IsRequired().HasMaxLength(100);
            entity.Property(o => o.CustomerPhone).IsRequired().HasMaxLength(20);
            entity.Property(o => o.FoodItem).IsRequired().HasMaxLength(100);
            entity.Property(o => o.DeliveryAddress).IsRequired().HasMaxLength(250);
        });

        modelBuilder.Entity<Order>().HasData(
            new Order
            {
                Id = 1,
                CustomerName = "Alice Johnson",
                CustomerPhone = "555-0101",
                FoodItem = "Chicken Burger",
                Quantity = 2,
                Price = 18.50m,
                DeliveryAddress = "123 Main Street, Springfield",
                Status = OrderStatus.Placed,
                OrderDate = new DateTime(2026, 7, 15, 12, 0, 0, DateTimeKind.Utc)
            },
            new Order
            {
                Id = 2,
                CustomerName = "Bob Smith",
                CustomerPhone = "555-0102",
                FoodItem = "Veggie Pizza",
                Quantity = 1,
                Price = 14.00m,
                DeliveryAddress = "456 Oak Avenue, Springfield",
                Status = OrderStatus.Preparing,
                OrderDate = new DateTime(2026, 7, 15, 13, 30, 0, DateTimeKind.Utc)
            });
    }
}
