using BISA.Shared.DTO;
using BISA.Shared.Entities;

namespace BISA.Client.Services.ReservationsService
{
    public interface IReservationsService
    {
        Task<List<LoanReservationViewModel>> GetMyReservations();
    }
}
