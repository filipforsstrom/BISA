using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BISA.Shared.Entities
{
    public class TagEntity
    {
        public int Id { get; set; }
        public string? Tag { get; set; }
        [JsonIgnore]
        public List<ItemEntity>? Items { get; set; }
        public List<ItemTagEntity> ItemTags { get; set; }

    }
}
