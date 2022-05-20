namespace BISA.Shared.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Organizer { get; set; }
        public string? Subject { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public EventTypeViewModel Type { get; set; }
    }
}
