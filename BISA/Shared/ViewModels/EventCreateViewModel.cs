using System.ComponentModel.DataAnnotations;

namespace BISA.Shared.ViewModels
{
    public class EventCreateViewModel
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public TimeSpan Time { get; set; }
        [Required]
        public string? Organizer { get; set; }
        [Required]
        public string? Subject { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int EventTypeId { get; set; }
    }
}
