using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.ViewModels
{
    public class EbookViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required!")]
        public string? Title { get; set; }
        public string? Language { get; set; }
        public string? Date { get; set; }
        public string? Publisher { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Creator { get; set; }
        public List<TagViewModel>? Tags { get; set; }
        public int ItemInventory { get; set; }
        public List<ItemInventoryViewModel> Inventory { get; set; }
        public string? Url { get; set; }
    }
}
