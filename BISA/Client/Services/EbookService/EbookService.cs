namespace BISA.Client.Services.EbookService
{
    public class EbookService : IEbookService
    {
        private readonly HttpClient _http;

        public EbookService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> CreateEbook(EbookViewModel ebookToCreate)
        {
            var response = await _http.PostAsJsonAsync("api/ebooks", ebookToCreate);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<EbookViewModel> GetEbook(int itemId)
        {
            var response = await _http.GetAsync($"api/ebooks/{itemId}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<EbookViewModel>();
            }
            else return null;
        }

        public async Task<string> UpdateEbook(EbookViewModel ebookToUpdate)
        {
            var response = await _http.PutAsJsonAsync($"api/ebooks/{ebookToUpdate.Id}", ebookToUpdate);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
