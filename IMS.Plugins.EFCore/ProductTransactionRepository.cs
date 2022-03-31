using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EFCore
{
    public class ProductTransactionRepository : IProductTransactionRepository
    {
        private readonly IMSContext db;
        private readonly IProductRepository repository;

        public ProductTransactionRepository(IMSContext db, IProductRepository repository)
        {
            this.db = db;
            this.repository = repository;
        }

        public async Task ProduceAsync(string productionNumber, Product product, int quantity, double price, string doneBy)
        {
            //var prod = await db.Products.Include(x => x.ProductInventories)
            //    .ThenInclude(x => x.Inventory)
            //    .FirstOrDefaultAsync(x => x.ProductId == product.ProductId);
            var prod = await repository.GetProductsByIdAsync(product.ProductId);
            if (prod is not null)
            {
                foreach (var pi in prod.ProductInventories)
                {
                    pi.Inventory.Quantity -= quantity * pi.InventoryQuantity;
                }
            }

            await this.db.ProductTransactions.AddAsync(new ProductTransaction
            {
                ProductionNumber = productionNumber,
                ProductId = product.ProductId,
                QuantityBefore = product.Quantity,
                QuantityAfter = product.Quantity + quantity,
                ActivityType = ProductTransactionType.ProduceProduct,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = price
            });

            await this.db.SaveChangesAsync();
        }

        public async Task SellProductAsync(string salesOrderNumber, Product product, int quantity, double price, string doneBy)
        {
            await this.db.ProductTransactions.AddAsync(new ProductTransaction
            {
                SalesOrderNumber = salesOrderNumber,
                ProductId = product.ProductId,
                QuantityBefore = product.Quantity,
                QuantityAfter = product.Quantity - quantity,
                ActivityType = ProductTransactionType.ProduceProduct,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = price
            });

            await this.db.SaveChangesAsync();
        }
    }
}
