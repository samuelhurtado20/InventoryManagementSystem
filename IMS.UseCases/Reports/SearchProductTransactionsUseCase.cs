using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;

namespace IMS.UseCases.Reports
{
    public class SearchProductTransactionsUseCase : ISearchProductTransactionsUseCase
    {
        private readonly IProductTransactionRepository repository;

        public SearchProductTransactionsUseCase(IProductTransactionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<ProductTransaction>> ExecuteAsync(
            string productName,
            DateTime? dateFrom,
            DateTime? dateTo,
            ProductTransactionType? transactionType
            )
        {
            return await this.repository.GetProductTransactionsAsync(productName, dateFrom, dateTo, transactionType);
        }
    }
}
