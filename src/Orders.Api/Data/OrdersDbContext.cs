using Microsoft.EntityFrameworkCore;
using Orders.Api.Models.Entities;

namespace Orders.Api.Data;
public class OrdersDbContext(DbContextOptions<OrdersDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
          modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.CustomerId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.TotalAmount)
                .HasPrecision(18, 2);

            entity.HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.ProductId)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(x => x.UnitPrice)
                .HasPrecision(18, 2);
        });    
    }
}