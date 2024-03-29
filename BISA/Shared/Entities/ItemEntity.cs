﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.Entities
{
    public class ItemEntity
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Language { get; set; }
        public string? Date { get; set; }
        public string? Type { get; set; }
        public string? Publisher { get; set; }
        public string? Creator { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<TagEntity>? Tags { get; set; }
        public List<ItemTagEntity> ItemTags { get; set; }
        public List<ItemInventoryEntity> ItemInventory { get; set; }

    }
}
