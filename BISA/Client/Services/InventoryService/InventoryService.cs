using BISA.Shared.DTO;

namespace BISA.Client.Services.InventoryService
{
    public class InventoryService : IInventoryService
    {
        private readonly HttpClient _http;

        public InventoryService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponseViewModel<string>> AddItemInventory(int amount, int itemId)
        {
            ServiceResponseViewModel<string> serviceResponse = new();

            ItemInventoryChangeDTO changedItem = new ItemInventoryChangeDTO
            {
                AmountToAdd = amount,
                ItemId = itemId
            };

            var response = await _http.PostAsJsonAsync("api/inventory", changedItem);

            if(response.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Message = "Inventory items has successfully been added";
            }
            else if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponseViewModel<string>> DeleteItemInventory(int id)
        {
            ServiceResponseViewModel<string> serviceResponse = new();
            var response = await _http.DeleteAsync($"api/inventory/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                serviceResponse.Message = "Deletion successful";
                serviceResponse.Success = true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
                serviceResponse.Success = false;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponseViewModel<List<ItemInventoryViewModel>>> GetItemInventory(int itemId)
        {
            ServiceResponseViewModel<List<ItemInventoryViewModel>> serviceResponse = new();
            var response = await _http.GetAsync($"api/inventory/{itemId}");

            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Data = await response.Content.ReadFromJsonAsync<List<ItemInventoryViewModel>>();
                serviceResponse.Success = true;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }

            return serviceResponse;
        }
    }
}
