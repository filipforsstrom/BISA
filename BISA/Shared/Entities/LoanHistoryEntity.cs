using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.Entities
{
    [Table("LoanHistory")]
    public class LoanHistoryEntity : LoanSuperEntity
    {
        public DateTime Date_Returned { get; set; }
        [ForeignKey(nameof(ItemInventory))]
        public int ItemInventoryId { get; set; }
        public ItemInventoryEntity? ItemInventory { get; set; }
    }
}
