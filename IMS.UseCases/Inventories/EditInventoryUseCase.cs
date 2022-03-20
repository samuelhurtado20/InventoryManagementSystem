using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class EditInventoryUseCase : IEditInventoryUseCase
    {
        private readonly IInventoryRepository repository;

        public EditInventoryUseCase(IInventoryRepository repository)
        {
            this.repository = repository;
        }

        public async Task ExecuteAsync(Inventory inventory)
        {
            await this.repository.UpdateInventoryAsync(inventory);
        }
    }
}
