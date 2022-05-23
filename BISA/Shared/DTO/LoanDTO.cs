using BISA.Shared.Entities;

namespace BISA.Shared.DTO
{
    public class LoanDTO
    {
        public int Id { get; set; }
        public DateTime Date_From { get; set; }
        public DateTime Date_To { get; set; }
        public string? User_Email { get; set; }
        public ItemEntity? Item { get; set; }
        public int InvItemId { get; set; }
    }
}
