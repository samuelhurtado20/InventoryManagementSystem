using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases
{
    public class ViewInventoriesByNameUseCase : IViewInventoriesByNameUseCase
    {
        public IInventoryRepository InventoryRepository { get; }

        public ViewInventoriesByNameUseCase(IInventoryRepository inventoryRepository)
        {
            InventoryRepository = inventoryRepository;
        }

        public async Task<IEnumerable<Inventory>> ExecuteAsync(string name = @"")
        {
            return await this.InventoryRepository.GetInventoriesByName(name);
        }
    }
}