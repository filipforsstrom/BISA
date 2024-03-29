﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.Entities
{
    [Table("LoanReservations")]
    public class LoanReservationEntity : LoanSuperEntity
    {
        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public ItemEntity? Item { get; set; }
    }
}
