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

        public async Task<IEnumerable<ProductTransaction>> GetProductTransactionsAsync(
            string productName, 
            DateTime? dateFrom, 
            DateTime? dateTo, 
            ProductTransactionType? transactionType)
        {
            var query = from pt in db.ProductTransactions
                        join prod in db.Products on pt.ProductId equals prod.ProductId
                        where (string.IsNullOrWhiteSpace(productName) || prod.ProductName.ToLower().IndexOf(productName.ToLower()) >= 0)
                        && (!dateFrom.HasValue || pt.TransactionDate >= dateFrom.Value.Date)
                        && (!dateTo.HasValue || pt.TransactionDate <= dateTo.Value.Date.AddDays(1))
                        && (!transactionType.HasValue || pt.ActivityType == transactionType)
                        select pt;
            return await query.Include(x => x.Product).ToListAsync();
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
                    int qtyBefore = pi.Inventory.Quantity;
                    pi.Inventory.Quantity -= quantity * pi.InventoryQuantity;

                    await this.db.InventoryTransactions.AddAsync(new CoreBusiness.InventoryTransaction
                    {
                        ProductionNumber = productionNumber,
                        InventoryId = pi.Inventory.InventoryId,
                        QuantityBefore = qtyBefore,
                        ActivityType = InventoryTransactionType.ProduceProduct,
                        QuantityAfter = pi.Inventory.Quantity,
                        TransactionDate = DateTime.UtcNow,
                        DoneBy = doneBy,
                        UnitPrice = price //* quantity
                    });
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
                ActivityType = ProductTransactionType.SellProduct,
                TransactionDate = DateTime.Now,
                DoneBy = doneBy,
                UnitPrice = price
            });

            await this.db.SaveChangesAsync();
        }
    }
}
