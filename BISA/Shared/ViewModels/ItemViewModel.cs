namespace BISA.Shared.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string? Type { get; set; }
        public List<ItemInventoryViewModel> Inventory { get; set; }
    }
}
