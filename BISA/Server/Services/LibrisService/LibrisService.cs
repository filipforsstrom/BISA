using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;
using System.Text.Json;

namespace BISA.Server.Services.LibrisService
{
    public class LibrisService : ILibrisService
    {
        private readonly BisaDbContext _bisaDbContext;
        private readonly HttpClient _http;

        public LibrisService(BisaDbContext bisaDbContext, HttpClient http)
        {
            _bisaDbContext = bisaDbContext ?? throw new ArgumentNullException(nameof(bisaDbContext));
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<List<LibrisItemDTO>> GetItem(string ISBN)
        {
            var result = await _http.GetStringAsync($"https://libris.kb.se/xsearch?query=ISBN:({ISBN})&format=json&n=200");
            var items = JsonToLibrisDTO(result);
            return items;
        }

        public async Task<List<LibrisItemDTO>> GetItems()
        {
            var result = await _http.GetStringAsync("https://libris.kb.se/xsearch?query=W.V.+Quine&format=json&n=200");
            var items = JsonToLibrisDTO(result);
            return items;
        }
        private List<LibrisItemDTO> JsonToLibrisDTO(string json)
        {
            List<LibrisItemDTO> items = new List<LibrisItemDTO>();
            using (JsonDocument document = JsonDocument.Parse(json))
            {
                JsonElement JsonItemsList = document.RootElement.GetProperty("xsearch").GetProperty("list");
                foreach (JsonElement JsonItem in JsonItemsList.EnumerateArray())
                {
                    LibrisItemDTO item = new LibrisItemDTO();
                    if (JsonItem.TryGetProperty("title", out JsonElement titleElement))
                    {
                        item.Title = titleElement.ToString();
                    }
                    if (JsonItem.TryGetProperty("creator", out JsonElement creatorElement))
                    {
                        item.Creator = creatorElement.ToString();
                    }
                    if (JsonItem.TryGetProperty("isbn", out JsonElement isbnElement))
                    {
                        if (isbnElement.ValueKind == JsonValueKind.Array)
                        {
                            item.ISBN = isbnElement[0].ToString();
                        }
                        else
                        {
                            item.ISBN = isbnElement.ToString();
                        }
                    }
                    if (JsonItem.TryGetProperty("type", out JsonElement typeElement))
                    {
                        item.Type = typeElement.ToString();
                    }
                    if (JsonItem.TryGetProperty("publisher", out JsonElement publisherElement))
                    {
                        if (publisherElement.ValueKind == JsonValueKind.Array)
                        {
                            item.Publisher = publisherElement[0].ToString();
                        }
                        else
                        {
                            item.Publisher = publisherElement.ToString();
                        }
                    }
                    if (JsonItem.TryGetProperty("date", out JsonElement dateElement))
                    {
                        if (dateElement.ValueKind == JsonValueKind.Array)
                        {
                            item.Date = FormatYear(dateElement[0].ToString());
                        }
                        else
                        {
                            item.Date = FormatYear(dateElement.ToString());
                        }
                    }
                    if (JsonItem.TryGetProperty("language", out JsonElement languageElement))
                    {
                        item.Language = languageElement.ToString();
                    }
                    items.Add(item);
                }
            }
            return items;
        }
        private string FormatYear(string json)
        {
            return new string(json.TakeWhile(char.IsDigit).ToArray());
        }
        private async Task AddBooksToDb(List<BookEntity> bookEntities)
        {
            if (bookEntities != null)
            {
                foreach (var bookEntity in bookEntities)
                {
                    _bisaDbContext.Add(bookEntity);
                    await _bisaDbContext.SaveChangesAsync();
                }
            }
        }
        private List<BookEntity> ConvertLibrisDTOsToBookEntities(List<LibrisItemDTO> items)
        {
            List<BookEntity> bookEntities = new List<BookEntity>();
            foreach (LibrisItemDTO item in items)
            {
                BookEntity bookEntity = new BookEntity
                {
                    Title = item.Title,
                    Language = item.Language,
                    Date = item.Date,
                    Publisher = item.Publisher,
                    Creator = item.Creator,
                    ISBN = item.ISBN,
                };
                bookEntities.Add(bookEntity);
            }
            return bookEntities;
        }
        private BookEntity ConvertLibrisDTOToBookEntity(LibrisItemDTO item)
        {
            BookEntity bookEntity = new BookEntity
            {
                Title = item.Title,
                Language = item.Language,
                Date = item.Date,
                Publisher = item.Publisher,
                Creator = item.Creator,
                ISBN = item.ISBN,
            };
            return bookEntity;
        }
        private MovieEntity ConvertLibrisDTOToMovieEntity(LibrisItemDTO item)
        {
            MovieEntity movieEntity = new MovieEntity
            {
                Title = item.Title,
                Language = item.Language,
                Date = item.Date,
                Publisher = item.Publisher,
                Creator = item.Creator,
            };
            return movieEntity;
        }
        private EbookEntity ConvertLibrisDTOToEbookEntity(LibrisItemDTO item)
        {
            EbookEntity ebookEntity = new EbookEntity
            {
                Title = item.Title,
                Language = item.Language,
                Date = item.Date,
                Publisher = item.Publisher,
                Creator = item.Creator,
            };
            return ebookEntity;
        }
        private async Task AddBookToDb(BookEntity bookEntity)
        {
            _bisaDbContext.Add(bookEntity);
            await _bisaDbContext.SaveChangesAsync();
        }
        private async Task AddEbookToDb(EbookEntity ebookEntity)
        {
            _bisaDbContext.Add(ebookEntity);
            await _bisaDbContext.SaveChangesAsync();
        }
        private async Task AddMovieToDb(MovieEntity movieEntity)
        {
            _bisaDbContext.Add(movieEntity);
            await _bisaDbContext.SaveChangesAsync();
        }

        public async Task SeedDatabase()
        {
            var librisItems = await GetItems();
            //var bookEntities = ConvertLibrisDTOsToBookEntities(librisItems);
            //await AddBooksToDb(bookEntities);
            await AddItemsToDb(librisItems);
        }
        private async Task AddItemsToDb(List<LibrisItemDTO> librisItems)
        {
            foreach (var item in librisItems)
            {
                if (item.Type == "book")
                {
                    var bookEntity = ConvertLibrisDTOToBookEntity(item);
                    await AddBookToDb(bookEntity);
                }
                else if (item.Type == "E-book")
                {
                    var ebookEntity = ConvertLibrisDTOToEbookEntity(item);
                    await AddEbookToDb(ebookEntity);
                }
                else if (item.Type == "moving imag")
                {
                    var movieEntity = ConvertLibrisDTOToMovieEntity(item);
                    await AddMovieToDb(movieEntity);
                }
            }
        }
    }
}
