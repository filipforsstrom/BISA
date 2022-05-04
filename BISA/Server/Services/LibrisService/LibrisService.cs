using BISA.Shared.Entities;
using System.Text.Json;

namespace BISA.Server.Services.LibrisService
{
    public class LibrisService : ILibrisService
    {
        private readonly HttpClient _http;

        public LibrisService(HttpClient http)
        {
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
                        item.Date = dateElement.ToString();
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
        private async Task AddItemsToDb(List<LibrisItemDTO> items)
        {

        }
    }
}
