using BISA.Shared.DTO;

namespace BISA.Client.Services.LoanService
{
    public class LoanService : ILoanService
    {
        private readonly HttpClient _httpClient;

        public LoanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<LoanViewModel>> GetAllLoans()
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponseViewModel<List<LoanViewModel>>> GetMyLoans()
        {
            ServiceResponseViewModel<List<LoanViewModel>> responseViewModel = new();

            var httpResponse = await _httpClient.GetAsync("api/loans/user");
            if (httpResponse.IsSuccessStatusCode)
            {
                responseViewModel.Success = true;
                responseViewModel.Data = await httpResponse.Content.ReadFromJsonAsync<List<LoanViewModel>>();

                return responseViewModel;
            }

            responseViewModel.Message = await httpResponse.Content.ReadAsStringAsync();
            responseViewModel.Success = false;
            return responseViewModel;
        }

        public async Task<ServiceResponseViewModel<List<LoanDTO>>> AddLoan(List<CheckoutDTO> items)
        {
            ServiceResponseViewModel<List<LoanDTO>> responseViewModel = new();

            var httpResponse = await _httpClient.PostAsJsonAsync("api/loans", items);
            if (httpResponse.IsSuccessStatusCode)
            {
                responseViewModel.Data = await httpResponse.Content.ReadFromJsonAsync<List<LoanDTO>>();
                responseViewModel.Success = true;

                return responseViewModel;
            }

            responseViewModel.Message = await httpResponse.Content.ReadAsStringAsync();
            responseViewModel.Success = false;
            return responseViewModel;
        }

        public async Task<ServiceResponseViewModel<string>> ReturnLoan(int id)
        {
            var response = new ServiceResponseViewModel<string>();

            var httpResponse = await _httpClient.DeleteAsync($"api/loans/{id}");
            if (httpResponse.IsSuccessStatusCode)
            {
                response.Message = "Item returned";
                response.Success = true;
            }
            else
            {
                var apiMessage = await httpResponse.Content.ReadAsStringAsync();
                response.Message = apiMessage;
                response.Success = false;
            }
            return response;
        }
    }
}
