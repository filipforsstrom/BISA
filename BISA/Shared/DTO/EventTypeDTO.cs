﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.DTO
{
    public class EventTypeDTO
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public int Capacity { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }

    }
}
