using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.Entities
{
    public class LoanEntity : LoanSuperEntity
    {
        [ForeignKey(nameof(ItemInventory))]
        public int ItemInventoryId { get; set; }
        public ItemInventoryEntity? ItemInventory { get; set; }
    }
}
