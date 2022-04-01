using IMS.CoreBusiness;
using IMS.UseCases.PluginInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.UseCases
{
    public class ValidateEnoughInventoriesForProducingUseCase : IValidateEnoughInventoriesForProducingUseCase
    {
        private readonly IProductRepository repository;

        public ValidateEnoughInventoriesForProducingUseCase(IProductRepository repository)
        {
            this.repository = repository;
        }

        public async Task<bool> ExecuteAsync(Product product, int quantity)
        {
            var prod = await repository.GetProductsByIdAsync(product.ProductId);
            foreach (var pi in prod.ProductInventories)
            {
                if (pi.InventoryQuantity * quantity > pi.Inventory.Quantity)
                    return false;
            }

            return true;
        }

        //public List<int> FixMe(List<List<int>> myList)
        //{
        //    List<int> newList = new List<int>();
        //    if (myList.Count % 2 == 0)
        //    { // imperative code
        //        foreach (List<int> item in myList)
        //        {
        //            foreach (int element in item)
        //            {
        //                newList.Add(element);
        //            }
        //        }
        //    }
        //    else
        //    {  // functional code
        //        newList.AddRange((IEnumerable<int>)myList.Select(x=>x.Count>0));
        //    }

        //    return newList.OrderBy(x => x).ToList();
        //}
    }
}
