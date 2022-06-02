namespace BISA.Shared.ViewModels
{
    public class ItemInventoryViewModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public bool Available { get; set; }
        public string Title { get; set; }
    }
}
