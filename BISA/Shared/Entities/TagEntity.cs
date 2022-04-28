using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.Entities
{
    public class TagEntity
    {
        public int Id { get; set; }
        public string? Tag { get; set; }

        public List<ItemEntity>? Items { get; set; }
    }
}
