using BISA.Shared.DTO;
using BISA.Shared.Entities;

namespace BISA.Client.Services.ReservationsService
{
    public interface IReservationsService
    {
        Task<ServiceResponseViewModel<List<LoanReservationViewModel>>> GetMyReservations();
        Task<ServiceResponseViewModel<string>> RemoveReservation(int reservationId);
        Task<ServiceResponseViewModel<List<LoanReservationViewModel>>> GetItemReservations(int itemId);
        Task<ServiceResponseViewModel<LoanReservationViewModel>> AddReservation(int itemId);
    }
}
