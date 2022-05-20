namespace BISA.Shared.DTO
{
    public class EventCreateDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string? Organizer { get; set; }
        public string? Subject { get; set; }
        public string? Location { get; set; }
        public string? Description { get; set; }
        public int EventTypeId { get; set; }
    }
}
