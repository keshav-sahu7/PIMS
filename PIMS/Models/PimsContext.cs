using Microsoft.EntityFrameworkCore;

namespace PIMS.Models;

public class PimsContext : DbContext
{
    public PimsContext(DbContextOptions<PimsContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define composite key for the many-to-many relationship
        modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.ProductId, pc.CategoryId });

        // Ensure SKU is unique
        modelBuilder.Entity<Product>()
            .HasIndex(p => p.SKU)
            .IsUnique();
    }
}
