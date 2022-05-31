namespace BISA.Shared.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Language { get; set; }
        public string? Date { get; set; }
        public string? Publisher { get; set; }
        public string? Creator { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<TagDTO>? Tags { get; set; }
        public int ItemInventory { get; set; }
        public List<ItemInventoryDTO> Inventory { get; set; }
        public int RuntimeInMinutes { get; set; }
    }
}
