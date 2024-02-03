using CoreBusiness;

using Microsoft.EntityFrameworkCore;

namespace Plugins.DataStore.SQL;

public class MarketContext : DbContext
{
    public MarketContext(DbContextOptions<MarketContext> options)
        : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>().HasMany<Product>(c => c.Products).WithOne(p => p.Category).HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Category>().HasData(
            new Category { CategoryId = 1, Name = "Beverage", Description = "Beverage" },
            new Category { CategoryId = 2, Name = "Bakery", Description = "Bakery" },
            new Category { CategoryId = 3, Name = "Meat", Description = "Meat" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product() { ProductId = 1, Name = "Iced Tea", CategoryId = 1, Quantity = 5, Price = 10.0 },
            new Product() { ProductId = 2, Name = "Product 2", CategoryId = 1, Quantity = 10, Price = 25.0 },
            new Product() { ProductId = 3, Name = "Product 3", CategoryId = 1, Quantity = 25, Price = 50.0 }
        );
    }
}
