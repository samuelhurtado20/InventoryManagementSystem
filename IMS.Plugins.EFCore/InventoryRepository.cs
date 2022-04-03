using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using Microsoft.EntityFrameworkCore;

namespace IMS.Plugins.EFCore
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly IMSContext db;

        public InventoryRepository(IMSContext db)
        {
            this.db = db;
        }

        public async Task AddInventoryAsync(Inventory inventory)
        {
            if (this.db.Inventories.Any(x => x.InventoryName.ToLower() == inventory.InventoryName.ToLower())) return;
            this.db.Inventories.Add(inventory);
            await this.db.SaveChangesAsync();
        }

        public async Task<Inventory> GetByIdAsync(int inventoryId)
        {
            var inv = await this.db.Inventories.FindAsync(inventoryId);
            if (inv is not null)
            {
                return inv;
            }
            return inv;
        }

        public async Task<IEnumerable<Inventory>> GetInventoriesByName(string name)
        {
            return await this.db.Inventories.Where(x => x.InventoryName.ToLower().IndexOf(name.ToLower()) >=0).ToListAsync();
            //return await this.db.Inventories.Where(x => x.InventoryName.ToLower().Contains(name.ToLower(), StringComparison.OrdinalIgnoreCase) || string.IsNullOrWhiteSpace(name)).ToListAsync();
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            //if (this.db.Inventories.Any(x => x.InventoryId != inventory.InventoryId && x.InventoryName.Equals(inventory.InventoryName, StringComparison.OrdinalIgnoreCase))) return;
            if (this.db.Inventories.Any(x => x.InventoryId != inventory.InventoryId 
                && x.InventoryName.ToLower() == inventory.InventoryName.ToLower())) return;
            var inv = await this.db.Inventories.FindAsync(inventory.InventoryId);
            if(inv is not null)
            {
                inv.InventoryName = inventory.InventoryName;
                inv.Quantity = inventory.Quantity;
                inv.Price = inventory.Price;

                this.db.Inventories.Update(inv);
                await this.db.SaveChangesAsync();
            }
        }
    }
}
