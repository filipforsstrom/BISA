using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;

namespace BISA.Server.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly BisaDbContext _context;

        public ReservationService(BisaDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponseDTO<LoanReservationEntity>> AddReservation(int itemId)
        {
            var response = new ServiceResponseDTO<LoanReservationEntity>();

            // get user
            // simulated user
            var simUser = await _context.Users.FirstOrDefaultAsync();

            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);

            if (item != null)
            {
                // check earliest available time
                var time = CheckTimeAvailable(item.Id);

                var newReservation = new LoanReservationEntity
                {
                    UserId = simUser.Id,
                    Date_From = time,
                    Date_To = time.AddDays(20)
                };
            }

            response.Success = false;
            response.Message = $"Item with id: {itemId} not found";
            return response;
        }

        public async Task<ServiceResponseDTO<List<LoanReservationEntity>>> GetItemReservations(int itemId)
        {
            var response = new ServiceResponseDTO<List<LoanReservationEntity>>();

            var reservations = await _context.LoanReservations
                .Where(lr => lr.ItemId == itemId)
                .ToListAsync();

            if (reservations != null)
            {
                response.Data = reservations;
                response.Success = true;
                return response;
            }
            response.Success = false;
            response.Message = "No reservations found";
            return response;
        }

        public async Task<ServiceResponseDTO<List<LoanReservationEntity>>> GetMyReservations()
        {
            var response = new ServiceResponseDTO<List<LoanReservationEntity>>();

            // simulated user
            var simUser = await _context.Users.FirstOrDefaultAsync();

            var reservations = await _context.LoanReservations
                .Where(lr => lr.UserId == simUser.Id)
                .ToListAsync();

            if (reservations != null)
            {
                response.Data = reservations;
                response.Success = true;
                return response;
            }

            response.Success = false;
            response.Message = "";
            return response;
        }

        private DateTime CheckTimeAvailable(int id)
        {
            return DateTime.Now;
        }
    }
}
