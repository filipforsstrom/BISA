namespace BISA.Shared.ViewModels
{
    public class BookViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Language { get; set; }
        public string? Date { get; set; }
        public string? Publisher { get; set; }
        public string? Creator { get; set; }
        public List<TagViewModel>? Tags { get; set; }
        public string? ISBN { get; set; }
    }
}
