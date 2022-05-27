using BISA.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.DTO
{
    public class EbookUpdateDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Language { get; set; }
        public string? Date { get; set; }
        public string? Publisher { get; set; }
        public string? Creator { get; set; }
        public List<TagViewModel>? Tags { get; set; }
        //public int ItemInventory { get; set; } behövs ej? om vi ändå inte ska uppdatera detta? samma i bookUpdateDTO isf
        public string? Url { get; set; }
    }
}
