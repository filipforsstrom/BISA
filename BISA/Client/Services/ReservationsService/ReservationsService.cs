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
        public async Task<List<LoanReservationViewModel>> GetMyReservations()
        {
            var httpReponse = await _httpClient.GetAsync("api/reservations/user");
            if (httpReponse.IsSuccessStatusCode)
            {
                var reservationsList = await httpReponse.Content.ReadFromJsonAsync<List<LoanReservationViewModel>>();
                return reservationsList;
            }

            return new List<LoanReservationViewModel>();
            
        }
    }
}
