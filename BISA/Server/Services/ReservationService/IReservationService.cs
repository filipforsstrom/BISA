using BISA.Shared.Entities;

namespace BISA.Server.Services.ReservationService
{
    public interface IReservationService
    {
        Task<ServiceResponseDTO<List<LoanReservationEntity>>> GetItemReservations(int id);
        Task<ServiceResponseDTO<List<LoanReservationEntity>>> GetMyReservations();
    }
}
