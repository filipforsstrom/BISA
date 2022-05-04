using BISA.Shared.Entities;

namespace BISA.Server.Services.LibrisService
{
    public class LibrisService : ILibrisService
    {
        private readonly HttpClient _http;

        public LibrisService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
        }

        public async Task<LibrisItemDTO> GetItems()
        {
            var result = await _http.GetFromJsonAsync<LibrisItemDTO>("https://libris.kb.se/xsearch?query=W.V.+Quine&format=json&n=3");
            return result;
        }
    }
}
