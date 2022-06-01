using BISA.Shared.DTO;
using BISA.Shared.Entities;

namespace BISA.Client.Services.ReservationsService
{
    public class ReservationsService : IReservationsService
    {
        private readonly HttpClient _httpClient;

        public ReservationsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponseViewModel<LoanReservationViewModel>> AddReservation(int itemId)
        {
            ServiceResponseViewModel<LoanReservationViewModel> serviceResponse = new();

            var httpResponse = await _httpClient.PostAsJsonAsync($"api/reservations/{itemId}", itemId);
            if (httpResponse.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Data = await httpResponse.Content.ReadFromJsonAsync<LoanReservationViewModel>();

                return serviceResponse;
            }
            serviceResponse.Success = false;
            serviceResponse.Message = await httpResponse.Content.ReadAsStringAsync();

            return serviceResponse;
        }


        public async Task<ServiceResponseViewModel<List<LoanReservationViewModel>>> GetMyReservations()
        {
            ServiceResponseViewModel<List<LoanReservationViewModel>> serviceResponse = new();

            var httpResponse = await _httpClient.GetAsync("api/reservations/user");
            if (httpResponse.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Data = await httpResponse.Content.ReadFromJsonAsync<List<LoanReservationViewModel>>();

                return serviceResponse;
            }
            serviceResponse.Success = false;
            serviceResponse.Data = new List<LoanReservationViewModel>();
            serviceResponse.Message = await httpResponse.Content.ReadAsStringAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponseViewModel<string>> RemoveReservation(int reservationId)
        {
            ServiceResponseViewModel<string> serviceResponse = new();
            var httpResponse = await _httpClient.DeleteAsync($"api/reservations/{reservationId}");

            if (httpResponse.IsSuccessStatusCode)
            {
                serviceResponse.Data = await httpResponse.Content.ReadAsStringAsync();
                serviceResponse.Success = true;
                return serviceResponse;
            }

            serviceResponse.Data = await httpResponse.Content.ReadAsStringAsync();
            serviceResponse.Success = false;
            return serviceResponse;
        }
    }
}
