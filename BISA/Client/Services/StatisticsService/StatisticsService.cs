using BISA.Shared.DTO;

namespace BISA.Client.Services.StatisticsService
{
    public class StatisticsService : IStatisticsService
    {
        private readonly HttpClient _httpClient;

        public StatisticsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ServiceResponseViewModel<ItemViewModel>> GetMostPopularItem()
        {
            ServiceResponseViewModel<ItemViewModel> responseViewModel = new ();

            var httpClientResponse = await _httpClient.GetAsync("api/statistics/popular");
            if (httpClientResponse.IsSuccessStatusCode)
            {
                
                responseViewModel.Data = await httpClientResponse.Content.ReadFromJsonAsync<ItemViewModel>();
                return responseViewModel;
            }
            responseViewModel.Data = null;  
            responseViewModel.Message = await httpClientResponse.Content.ReadAsStringAsync();
            responseViewModel.Success = false;
            return responseViewModel;
        }

        public async Task<ServiceResponseViewModel<UserStatisticsViewModel>> GetMostActiveUser()
        {
            ServiceResponseViewModel<UserStatisticsViewModel> responseViewModel = new ();

            var httpClientResponse = await _httpClient.GetAsync("api/statistics/users");
            if (httpClientResponse.IsSuccessStatusCode)
            {
                responseViewModel.Data = await httpClientResponse.Content.ReadFromJsonAsync<UserStatisticsViewModel>();
                return responseViewModel;
            }

            responseViewModel.Data = null;
            responseViewModel.Message = await httpClientResponse.Content.ReadAsStringAsync();
            responseViewModel.Success = false;
            return responseViewModel;
        }

        public async Task<ServiceResponseViewModel<MostPopularAuthorViewModel>> GetMostPopularAuthor()
        {
            ServiceResponseViewModel<MostPopularAuthorViewModel> responseViewModel = new();

            var httpClientResponse = _httpClient.GetAsync("api/statistics/author").Result;

            if (httpClientResponse.IsSuccessStatusCode)
            {
                responseViewModel.Data = await httpClientResponse.Content.ReadFromJsonAsync<MostPopularAuthorViewModel>();
                return responseViewModel;
            }

            responseViewModel.Data = null;
            responseViewModel.Message = await httpClientResponse.Content.ReadAsStringAsync();
            responseViewModel.Success = false;
            return responseViewModel;
        }
    }
}
