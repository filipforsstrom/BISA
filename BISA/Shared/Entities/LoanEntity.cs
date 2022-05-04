using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.Entities
{
    public class LoanEntity
    {
        public int Id { get; set; }
        public DateTime Date_From { get; set; }
        public DateTime Date_To { get; set; }
        public DateTime Date_Returned { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        [ForeignKey(nameof(ItemInventory))]
        public int ItemInventoryId { get; set; }
        public ItemInventoryEntity? ItemInventory { get; set; }

    }
}
