using BISA.Shared.Entities;

namespace BISA.Shared.ViewModels
{
    public class LoanViewModel
    {
        public int Id { get; set; }
        public DateTime Date_From { get; set; }
        public DateTime Date_To { get; set; }
        public string? UserId { get; set; }
        public ItemEntity? Item { get; set; }
        public int ItemInventoryId { get; set; }
    }
}
