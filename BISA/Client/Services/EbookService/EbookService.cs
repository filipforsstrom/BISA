using static System.Net.HttpStatusCode;

namespace BISA.Client.Services.EbookService
{
    public class EbookService : IEbookService
    {
        private readonly HttpClient _http;

        public EbookService(HttpClient http)
        {
            _http = http;
        }

        public async Task<ServiceResponseViewModel<string>> CreateEbook(EbookViewModel ebookToCreate)
        {
            ServiceResponseViewModel<string> serviceResponse = new();
            var response = await _http.PostAsJsonAsync("api/ebooks", ebookToCreate);
            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Message = "Ebook successfully created";
            }
            else if (response.StatusCode == BadRequest)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }
            else if (response.StatusCode == InternalServerError)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }
            return serviceResponse;
        }

        public async Task<ServiceResponseViewModel<EbookViewModel>> GetEbook(int itemId)
        {
            ServiceResponseViewModel<EbookViewModel> serviceResponse = new();
            var response = await _http.GetAsync($"api/ebooks/{itemId}");
            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Data = await response.Content.ReadFromJsonAsync<EbookViewModel>();
                serviceResponse.Success = true;
            }
            else if (response.StatusCode == BadRequest)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }
            return serviceResponse;
        }

        public async Task<ServiceResponseViewModel<string>> UpdateEbook(EbookViewModel ebookToUpdate)
        {
            ServiceResponseViewModel<string> serviceResponse = new();
            var response = await _http.PutAsJsonAsync($"api/ebooks/{ebookToUpdate.Id}", ebookToUpdate);
            if (response.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Message = "Ebook successfully updated";
            }
            else if (response.StatusCode == BadRequest)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }
            else if (response.StatusCode == InternalServerError)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = await response.Content.ReadAsStringAsync();
            }
            return serviceResponse;
        }
    }
}
