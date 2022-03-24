using IMS.CoreBusiness;

namespace IMS.UseCases
{
    public interface IDeleteProductUseCase
    {
        Task ExecuteAsync(Product product);
    }
}