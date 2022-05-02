using BISA.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.DTO
{
    public class SearchDTO
    {
        public string? Title { get; set; }
        public string? Language { get; set; }
        public string? Date { get; set; }
        public string? Publisher { get; set; }
        public string? Creator { get; set; }
        public string? ISBN { get; set; }

        // TYPE String? Int? Bool?
        public List<TagEntity>? Tags { get; set; }

    }
}
