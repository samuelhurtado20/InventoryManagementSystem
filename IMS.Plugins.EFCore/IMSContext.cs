using IMS.CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class IMSContext : DbContext
    {
        public IMSContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }
        public DbSet<ProductTransaction> ProductTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // relations
            modelBuilder.Entity<ProductInventory>()
                .HasKey(pi => new { pi.ProductId, pi.InventoryId });

            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductInventories)
                .HasForeignKey(p => p.ProductId);

            modelBuilder.Entity<ProductInventory>()
                .HasOne(pi => pi.Inventory)
                .WithMany(p => p.ProductInventories)
                .HasForeignKey(p => p.InventoryId);

            //seeding data
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { InventoryId = 1, InventoryName = "Engine", Price = 1000, Quantity = 1 },
                new Inventory { InventoryId = 2, InventoryName = "Body", Price = 400, Quantity = 1 },
                new Inventory { InventoryId = 3, InventoryName = "Wheels", Price = 100, Quantity = 4 },
                new Inventory { InventoryId = 4, InventoryName = "Seat", Price = 50, Quantity = 5 }
                );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, ProductName = "Gas car", Price = 20000, Quantity = 1, IsActive=true },
                new Product { ProductId = 2, ProductName = "Electric car", Price = 15000, Quantity = 1,IsActive=true }
                );


            modelBuilder.Entity<ProductInventory>().HasData(
                new ProductInventory { ProductId = 1, InventoryId = 1, InventoryQuantity = 11 },
                new ProductInventory { ProductId = 1, InventoryId = 2, InventoryQuantity = 12 },
                new ProductInventory { ProductId = 1, InventoryId = 3, InventoryQuantity = 13 },
                new ProductInventory { ProductId = 1, InventoryId = 4, InventoryQuantity = 14 },

                new ProductInventory { ProductId = 2, InventoryId = 1, InventoryQuantity = 21 },
                new ProductInventory { ProductId = 2, InventoryId = 2, InventoryQuantity = 22 },
                new ProductInventory { ProductId = 2, InventoryId = 3, InventoryQuantity = 23 },
                new ProductInventory { ProductId = 2, InventoryId = 4, InventoryQuantity = 24 }

                );
        }
    }
}
