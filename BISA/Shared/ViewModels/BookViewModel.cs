﻿using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;

namespace BISA.Shared.ViewModels
{
    public class BookViewModel
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

        [Required(ErrorMessage = "ISBN is required")]
        [MinLength(10, ErrorMessage = "Min. length for ISBN is 10.")]
        public string? ISBN { get; set; }
    }
}
