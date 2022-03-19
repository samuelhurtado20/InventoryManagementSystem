using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases
{
    public interface IViewInventoriesByNameUseCase
    {
        IInventoryRepository InventoryRepository { get; }

        Task<IEnumerable<Inventory>> ExecuteAsync(string name = "");
    }
}