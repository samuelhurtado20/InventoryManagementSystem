using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class EditProductUseCase : IEditProductUseCase
    {
        private readonly IProductRepository repository;

        public EditProductUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task ExecuteAsync(Product product)
        {
            await this.repository.UpdateProductAsync(product);
        }
    }
}
