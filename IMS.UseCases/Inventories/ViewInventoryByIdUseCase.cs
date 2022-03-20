using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class ViewInventoryByIdUseCase : IViewInventoryByIdUseCase
    {
        private readonly IInventoryRepository repository;

        public ViewInventoryByIdUseCase(IInventoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Inventory?> ExecuseAsync(int inventoryId)
        {
            return await this.repository.GetByIdAsync(inventoryId);
        }
    }
}
