namespace BISA.Shared.Entities
{
    public class ItemTagEntity
    {
        public int ItemId { get; set; }
        public ItemEntity Item { get; set; }
        public int TagId { get; set; }
        public TagEntity Tag { get; set; }
    }
}
