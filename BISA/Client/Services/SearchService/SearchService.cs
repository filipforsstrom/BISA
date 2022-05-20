namespace BISA.Client.Services.SearchService
{
    public class SearchService : ISearchService
    {
        private readonly HttpClient _http;

        public SearchService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ItemViewModel>> GetByTitle(string title)
        {
            var response = await _http.GetAsync($"api/search/title?title={title}");
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadFromJsonAsync<List<ItemViewModel>>();
                return list;
            }
            else return null;

        }
    }
}
