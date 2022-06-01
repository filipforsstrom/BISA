namespace BISA.Shared.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public string? Title { get; set; }
        public string? Language { get; set; }
        public string? Date { get; set; }
        public string? Publisher { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Creator { get; set; }
        public List<ItemInventoryViewModel>? Inventory { get; set; }
        public List<TagViewModel>? Tags { get; set; }



    }
}
