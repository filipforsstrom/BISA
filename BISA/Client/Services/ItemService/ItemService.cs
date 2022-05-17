namespace BISA.Client.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly HttpClient _http;

        public ItemService(HttpClient http)
        {
            _http = http;
        }

        public Task<ItemViewModel> DeleteItem(int id)
        {
            throw new NotImplementedException();
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

        public Task<List<ItemViewModel>> GetItems()
        {
            throw new NotImplementedException();
        }
    }
}
