namespace BISA.Client.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly HttpClient _http;

        public ItemService(HttpClient http)
        {
            _http = http;
        }

        public async Task<string> DeleteItem(int id)
        {
            var response = await _http.DeleteAsync($"api/items/{id}");

            if(response.StatusCode != System.Net.HttpStatusCode.NoContent)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return "Item deleted";
            }
            
        }

        public async Task<ItemViewModel> GetItem(int id)
        {
            var response = await _http.GetAsync($"api/items/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ItemViewModel>();
            }
            else return null;
        }

        public async Task<List<ItemViewModel>> GetItems()
        {
            var response = await _http.GetAsync("api/items");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<ItemViewModel>>();
            }
            else return null;

            
        }

        public async Task<List<TagViewModel>> GetTags()
        {
            var response = await _http.GetAsync("api/items/tags");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<TagViewModel>>();
            }
            else return null;
        }
    }
}
