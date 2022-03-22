using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class AddProductUseCase : IAddProductUseCase
    {
        private readonly IProductRepository repository;

        public AddProductUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task ExecuteAsync(Product product)
        {
            if (product is null) return;

            await repository.AddProductAsync(product);
        }
    }
}
