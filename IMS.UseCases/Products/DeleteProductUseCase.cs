using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductRepository repository;

        public DeleteProductUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task ExecuteAsync(Product product)
        {
            if (product is null) return;

            await repository.DeleteAsync(product);
        }
    }
}
