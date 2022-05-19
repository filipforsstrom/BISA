namespace BISA.Shared.ViewModels
{
    public class LoanViewModel
    {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string? UserId { get; set; }
        public int ItemId { get; set; }
        public int ItemInventoryId { get; set; }
    }
}
