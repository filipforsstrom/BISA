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
        public async Task<ServiceResponseViewModel<List<LoanReservationViewModel>>> GetMyReservations()
        {
            ServiceResponseViewModel<List<LoanReservationViewModel>> serviceResponse = new();

            var httpReponse = await _httpClient.GetAsync("api/reservations/user");
            if (httpReponse.IsSuccessStatusCode)
            {
                serviceResponse.Success = true;
                serviceResponse.Data = await httpReponse.Content.ReadFromJsonAsync<List<LoanReservationViewModel>>();
                
                return serviceResponse;
            }
            serviceResponse.Success = false;
            serviceResponse.Message = await httpReponse.Content.ReadAsStringAsync();

            return serviceResponse;
            
        }

        public async Task<string> RemoveReservation(int reservationId)
        {

            var httpResponse = await _httpClient.DeleteAsync($"api/reservations/{reservationId}");

            if (httpResponse.IsSuccessStatusCode)
            {
                return await httpResponse.Content.ReadAsStringAsync();
            }
           
            return await httpResponse.Content.ReadAsStringAsync();
        }
    }
}
