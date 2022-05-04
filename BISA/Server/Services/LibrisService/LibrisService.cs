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

        public async Task<List<LibrisItemDTO>> GetItems()
        {
            var result = await _http.GetStringAsync("https://libris.kb.se/xsearch?query=W.V.+Quine&format=json&n=200");
            List<LibrisItemDTO> items = new List<LibrisItemDTO>();

            using (JsonDocument document = JsonDocument.Parse(result))
            {
                JsonElement JSONitemsList = document.RootElement.GetProperty("xsearch").GetProperty("list");
                foreach (JsonElement JSONitem in JSONitemsList.EnumerateArray())
                {
                    LibrisItemDTO item = new LibrisItemDTO();
                    if (JSONitem.TryGetProperty("title", out JsonElement titleElement))
                    {
                        item.Title = titleElement.ToString();
                    }
                    if (JSONitem.TryGetProperty("creator", out JsonElement creatorElement))
                    {
                        item.Creator = creatorElement.ToString();
                    }
                    if (JSONitem.TryGetProperty("isbn", out JsonElement isbnElement))
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
                    if (JSONitem.TryGetProperty("type", out JsonElement typeElement))
                    {
                        item.Type = typeElement.ToString();
                    }
                    if (JSONitem.TryGetProperty("publisher", out JsonElement publisherElement))
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
                    if (JSONitem.TryGetProperty("date", out JsonElement dateElement))
                    {
                        item.Date = dateElement.ToString();
                    }
                    if (JSONitem.TryGetProperty("language", out JsonElement languageElement))
                    {
                        item.Language = languageElement.ToString();
                    }
                    items.Add(item);
                }
            }

            return items;
        }
    }
}
