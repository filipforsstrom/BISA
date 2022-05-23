using BISA.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.ViewModels
{
    public class LoanReservationViewModel
    {
        public int Id { get; set; }
        public DateTime Date_From { get; set; }
        public DateTime Date_To { get; set; }
        public int ItemId { get; set; }
        public ItemEntity? Item { get; set; }
        public int UserId { get; set; }
        public UserEntity? User { get; set; }

    }
}
