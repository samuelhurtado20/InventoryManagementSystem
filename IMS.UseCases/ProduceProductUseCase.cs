using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class ProduceProductUseCase : IProduceProductUseCase
    {
        private readonly IInventoryRepository repository;
        private readonly IProductRepository productRepository;
        private readonly IInventoryTransactionRepository inventoryTransactionRepository;
        private readonly IProductTransactionRepository productTransactionRepository;

        public ProduceProductUseCase(IInventoryRepository inventoryRepository,
            IProductRepository productRepository,
            IInventoryTransactionRepository inventoryTransactionRepository, IProductTransactionRepository productTransactionRepository)
        {
            this.repository = inventoryRepository;
            this.productRepository = productRepository;
            this.inventoryTransactionRepository = inventoryTransactionRepository;
            this.productTransactionRepository = productTransactionRepository;
        }

        public async Task ExecuteAsync(string productionNumber, Product product, int quantity, double price, string doneBy)
        {
            await this.productTransactionRepository.ProduceAsync(productionNumber, product, quantity, price, doneBy);

            product.Quantity += quantity;
            await this.productRepository.UpdateProductAsync(product);
        }
    }
}
