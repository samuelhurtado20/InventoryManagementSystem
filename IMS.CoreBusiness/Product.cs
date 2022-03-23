using IMS.CoreBusiness.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = string.Empty;

        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be greater or equal to {0}")]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be greater or equal to {0}")]
        [ProductPriceGreaterThanInventoriesPrice]
        public double Price { get; set; }

        public List<ProductInventory>? ProductInventories { get; set; }

        public double TotalInventoryCost()
        {
            if (this.ProductInventories == null || this.ProductInventories.Count <= 0) return 0;
            return this.ProductInventories.Sum(x => x.Inventory?.Price * x.InventoryQuantity ?? 0);
        }

        public bool ValidatePricing()
        {
            if (this.ProductInventories == null || this.ProductInventories.Count <= 0) return true;

            //double priceOfAllInvs = this.ProductInventories.Sum(x => x.Inventory?.Price * x.InventoryQuantity ?? 0);
            return this.TotalInventoryCost() <= this.Price;
        }
    }
}
