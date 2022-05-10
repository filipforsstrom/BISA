using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.DTO
{
    public class LoanDTO
    {
        public int Id { get; set; }
        public DateTime Date_From { get; set; }
        public DateTime Date_To { get; set; }
        public string? User_Email { get; set; }
        public int ItemId { get; set; }
        public int InvItemId { get; set; }
    }
}
