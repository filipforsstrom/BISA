using BISA.Shared.Entities;

namespace BISA.Server.Services.ReservationService
{
    public interface IReservationService
    {
        Task<List<LoanReservationEntity>> GetItemReservations(int itemId);
        Task<List<LoanReservationEntity>> GetMyReservations();
        Task<LoanReservationEntity> AddReservation(int itemId);
        Task RemoveReservation(int id);
    }
}
