using IMS.CoreBusiness;
using IMS.UseCases;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class InventoryTransactionRepository : IInventoryTransactionRepository
    {
        private readonly IMSContext db;

        public InventoryTransactionRepository(IMSContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<InventoryTransaction>> GetInventoryTransactionsAsync(string inventoryName, DateTime? dateFrom, DateTime? dateTo, InventoryTransactionType? transactionType)
        {
            //return await this.db.InventoryTransactions.Where(x=>x.Inventory.InventoryName == inventoryName && );

            var query = from it in db.InventoryTransactions
                        join inv in db.Inventories on it.InventoryId equals inv.InventoryId
                        where (string.IsNullOrWhiteSpace(inventoryName) || inv.InventoryName.ToLower().IndexOf(inventoryName.ToLower()) >= 0)
                        && (!dateFrom.HasValue || it.TransactionDate >= dateFrom.Value.Date)
                        && (!dateTo.HasValue || it.TransactionDate <= dateTo.Value.Date)
                        && (!transactionType.HasValue || it.ActivityType == transactionType)
                        select it;
            return await query.Include(x => x.Inventory).ToListAsync();
        }

        public async Task PurchaseAsync(string poNumber, Inventory inventory, int quantity, double price, string doneBy)
        {
            await this.db.InventoryTransactions.AddAsync(new CoreBusiness.InventoryTransaction
            {
                PONumber = poNumber,
                InventoryId = inventory.InventoryId,
                QuantityBefore = inventory.Quantity,
                ActivityType = InventoryTransactionType.PurchaseInventory,
                QuantityAfter = inventory.Quantity + quantity,
                TransactionDate = DateTime.UtcNow,
                DoneBy = doneBy,
                UnitPrice = price //* quantity
            });

            await this.db.SaveChangesAsync();
        }
    }
}
