using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;

namespace BISA.Server.Services.ReservationService
{
    public class ReservationService : IReservationService
    {
        private readonly BisaDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public ReservationService(BisaDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<ServiceResponseDTO<LoanReservationEntity>> AddReservation(int itemId)
        {
            var response = new ServiceResponseDTO<LoanReservationEntity>();

            var userIdFromToken = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userInDb = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userIdFromToken);

            if (userInDb == null)
            {
                response.Success = false;
                response.Message = "No matching user";
                return response;
            }
            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);

            if (item != null)
            {
                var duplicateCheck = await _context.LoansReservation
                    .Where(lr => lr.ItemId == itemId && lr.UserId == userInDb.Id)
                    .FirstOrDefaultAsync();

                if (duplicateCheck == null)
                {
                    // check earliest available time
                    var time = CheckTimeAvailable(item.Id);

                    var newReservation = new LoanReservationEntity
                    {
                        UserId = userInDb.Id,
                        Date_From = time,
                        Date_To = time.AddDays(20),
                        ItemId = item.Id
                    };

                    _context.LoansReservation.Add(newReservation);
                    await _context.SaveChangesAsync();

                    response.Data = newReservation;
                    response.Success = true;
                    return response;
                }

                response.Success = false;
                response.Message = $"Item with id: {itemId} allready reserved by user";
                return response;
            }

            response.Success = false;
            response.Message = $"Item with id: {itemId} not found";
            return response;
        }

        public async Task<ServiceResponseDTO<List<LoanReservationEntity>>> GetItemReservations(int itemId)
        {
            var response = new ServiceResponseDTO<List<LoanReservationEntity>>();

            var reservations = await _context.LoansReservation
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

            var userIdFromToken = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userInDb = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userIdFromToken);

            if (userInDb == null)
            {
                response.Success = false;
                response.Message = "No matching user";
                return response;
            }

            var reservations = await _context.LoansReservation
                .Include(lrItem => lrItem.Item)
                .Where(lr => lr.UserId == userInDb.Id)
                .ToListAsync();

            if (reservations != null)
            {
                response.Data = reservations;
                response.Success = true;
                return response;
            }

            response.Success = false;
            response.Message = "Request failed";
            return response;
        }

        public async Task<ServiceResponseDTO<string>> RemoveReservation(int id)
        {
            var response = new ServiceResponseDTO<string>();

            var userIdFromToken = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userInDb = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userIdFromToken);

            if (userInDb == null)
            {
                response.Success = false;
                response.Message = "No matching user";
                return response;
            }

            var reservationToRemove = await _context.LoansReservation
                .Where(lr => lr.Id == id)
                .FirstOrDefaultAsync();

            if (reservationToRemove != null)
            {
                if (userInDb.Id == reservationToRemove.UserId)
                {
                    _context.LoansReservation.Remove(reservationToRemove);
                    await _context.SaveChangesAsync();

                    response.Success = true;
                    response.Data = $"Reservation {id} canceled";
                    return response;
                }

                response.Success = false;
                response.Message = "Invalid user";
                return response;
            }

            response.Success = false;
            response.Message = "No matching reservation found";
            return response;
        }

        private DateTime CheckTimeAvailable(int id)
        {
            // Check earlier reservations
            var loans = _context.LoansActive
                .Where(l => l.ItemInventory.ItemId == id)
                .OrderBy(l => l.Date_To)
                .ToList();

            var reservations = _context.LoansReservation
                .Where(lr => lr.ItemId == id).OrderBy(lr => lr.Date_To)
                .ToList();

            if (reservations != null)
            {
                var itemInventory = _context.ItemInventory.Where(i => i.ItemId == id).ToList();
                var newReservationIndex = reservations.Count();
                var numberOfInventory = itemInventory.Count();
                if (numberOfInventory > reservations.Count())
                {
                    var earliestLoanItem = loans[newReservationIndex];
                    return earliestLoanItem.Date_To.AddDays(1);
                }

                var earliestReservationItem = reservations[newReservationIndex - numberOfInventory];
                return earliestReservationItem.Date_To.AddDays(1);
                // Calculate estimated time of earliest available invItem
                // return time
                //return DateTime.Now;
            }
            else
            {
                return loans[0].Date_To.AddDays(1);
            }
        }

    }
}
