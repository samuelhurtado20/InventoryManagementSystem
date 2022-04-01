using IMS.CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases.Reports
{
    public class SearchInventoryTransactionsUseCase : ISearchInventoryTransactionsUseCase
    {
        private readonly IInventoryTransactionRepository repository;

        public SearchInventoryTransactionsUseCase(IInventoryTransactionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<InventoryTransaction>> ExecuteAsync(
            string inventoryName,
            DateTime? dateFrom,
            DateTime? dateTo,
            InventoryTransactionType? transactionType
            )
        {
            return await this.repository.GetInventoryTransactionsAsync(inventoryName, dateFrom, dateTo, transactionType);
        }
    }
}
