﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.DTO
{
    public class ItemInventoryChangeDTO
    {
        public int InventoryId { get; set; }
        public int AmountToAdd { get; set; }
        public int ItemId { get; set; }
    }
}
