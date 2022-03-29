using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Plugins.EFCore
{
    internal class ProductTransactionRepository : IProductTransactionRepository
    {
        private readonly IMSContext db;

        public ProductTransactionRepository(IMSContext db)
        {
            this.db = db;
        }

        public async Task ProduceAsync(string productionNumber, Product product, int quantity, string doneBy)
        {
            await this.db.ProductTransactions.AddAsync(new ProductTransaction
            {

            });
        }
    }
}
