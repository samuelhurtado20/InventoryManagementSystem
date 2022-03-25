using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface IInventoryTransactionRepository
    {
        Task PurchaseAsync(string poNumber, Inventory inventory, int quatity, double price, string doneBy);
    }
}