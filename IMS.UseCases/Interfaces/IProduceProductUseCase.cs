using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface IProduceProductUseCase
    {
        Task ExecuteAsync(string productionNumber, Product product, int quantity, double price, string doneBy);
    }
}