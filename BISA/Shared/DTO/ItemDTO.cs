using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.DTO
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Language { get; set; }
        public string? Date { get; set; }
        public string? Publisher { get; set; }
        public string? Creator { get; set; }
        public string? Type { get; set; }
        public List<TagDTO> Tags { get; set; } // i ItemService omvandlar vi List<TagEntity> till en string array?? diskussion
        public int ItemInventory { get; set; } // i ItemService omvandlar vi  List<ItemInventoryEntity> till int

    }
}
