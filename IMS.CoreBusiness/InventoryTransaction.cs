using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.CoreBusiness
{
    public class InventoryTransaction
    {
        public int InventoryTransactionId { get; set; }

        [Required]
        public int InventoryId { get; set; }
        [Required]
        public int QuantityBefore { get; set; }

        //purchase or product
        [Required]
        public InventoryTransactionType ActivityType { get; set; }

        [Required]
        public int QuantityAfter { get; set; }
        public string PONumber { get; set; }
        public string ProductionNumber { get; set; }

        public double? UnitPrice { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        public string DoneBy { get; set; } = string.Empty;

        //navigation properties
        public Inventory Inventory { get; set; }
    }
}
