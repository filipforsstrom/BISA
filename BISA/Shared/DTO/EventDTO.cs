using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.DTO
{
    public class EventDTO
    {
        public DateTime Date { get; set; }
        public string? Organizer { get; set; }
        public string? Subject { get; set; }
        public string? Location { get; set; }
        public int EventTypeId { get; set; }
    }
}
