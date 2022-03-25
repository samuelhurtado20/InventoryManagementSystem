using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface IPurchaseInventoryUseCase
    {
        Task ExecuteAsync(string poNumber, Inventory inventory, int quatity, string doneBy);
    }
}