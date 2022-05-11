using BISA.Shared.Entities;

namespace BISA.Server.Services.ReservationService
{
    public interface IReservationService
    {
        Task<ServiceResponseDTO<List<LoanReservationEntity>>> GetItemReservations(int itemId);
        Task<ServiceResponseDTO<List<LoanReservationEntity>>> GetMyReservations();
        Task<ServiceResponseDTO<LoanReservationEntity>> AddReservation(int itemId);
        Task<ServiceResponseDTO<string>> RemoveReservation(int id);
    }
}
