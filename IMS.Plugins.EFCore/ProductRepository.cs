using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMSContext db;

        public ProductRepository(IMSContext db)
        {
            this.db = db;
        }

        public async Task AddProductAsync(Product product)
        {
            if (this.db.Products.Any(x => x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase))) return;
            product.IsActive = true;
            this.db.Products.Add(product);
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            var prod = await this.db.Products.FindAsync(product.ProductId);
            if (prod is not null)
            {
                //this.db.Products.Remove(prod);
                prod.IsActive = false;
                this.db.Products.Update(prod);
                await this.db.SaveChangesAsync();
            }
        }

        public async Task<Product?> GetProductsByIdAsync(int productId)
        {
            return await this.db.Products.Include(x => x.ProductInventories).ThenInclude(x => x.Inventory)
                .FirstOrDefaultAsync(x=>x.ProductId == productId);
        }

        public async Task<List<Product>> GetProductsByNameAsync(string name)
        {
            return await this.db.Products.Where(x => (x.ProductName.Contains(name, StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(name)) && x.IsActive).ToListAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            if (this.db.Products.Any(x => x.ProductId != product.ProductId && x.ProductName.Equals(product.ProductName, StringComparison.OrdinalIgnoreCase))) return;
            var prod = await this.db.Products.FindAsync(product.ProductId);
            if (prod is not null)
            {
                prod.ProductName = product.ProductName;
                prod.Quantity = product.Quantity;
                prod.Price = product.Price;

                this.db.Products.Update(prod);
                await this.db.SaveChangesAsync();
            }
        }
    }
}
