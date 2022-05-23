using BISA.Shared.DTO;

namespace BISA.Client.Services.SearchService
{
    public class SearchService : ISearchService
    {
        private readonly HttpClient _http;

        public SearchService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ItemViewModel>> GetByTitle(SearchDTO search)
        {
            var response = await _http.GetAsync($"api/search/title?title={search.UserSearch}");
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadFromJsonAsync<List<ItemViewModel>>();
                return list;
            }
            else return null;

        }

        public async Task<List<ItemViewModel>> GetByTags(SearchDTO search)
        {
            var response = await _http.GetAsync($"api/search/tag?tag={search.UserSearch}");
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadFromJsonAsync<List<ItemViewModel>>();
                return list;
            }
            else return null;
        }

        public async Task<List<ItemViewModel>> GetByAll(SearchDTO search)
        {
            var response = await _http.GetAsync($"api/search/all?search={search.UserSearch}");
            if (response.IsSuccessStatusCode)
            {
                var list = await response.Content.ReadFromJsonAsync<List<ItemViewModel>>();
                return list;
            }
            else return null;
        }
    }
}
