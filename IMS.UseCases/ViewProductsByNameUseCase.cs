using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class ViewProductsByNameUseCase : IViewProductsByNameUseCase
    {
        private readonly IProductRepository repository;

        public ViewProductsByNameUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<List<Product>> ExecuteAsync(string name = "")
        {
            return await this.repository.GetProductsByName(name);
        }
    }
}
