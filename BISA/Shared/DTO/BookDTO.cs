using BISA.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Language { get; set; }
        public string? Date { get; set; }
        public string? Type { get; set; }
        public string? Publisher { get; set; }
        public string? Creator { get; set; }
        public List<TagEntity>? Tags { get; set; }
        public List<ItemInventoryEntity> ItemInventory { get; set; }
        public string? ISBN { get; set; }

    }
}
