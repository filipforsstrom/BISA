using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BISA.Shared.Entities
{
    public class EventEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Organizer { get; set; }
        public string? Subject { get; set; }
        public string? Location { get; set; }

        [ForeignKey(nameof(EventType))]
        public int EventTypeId { get; set; }
        public EventTypeEntity? EventType { get; set; }
    }
}
