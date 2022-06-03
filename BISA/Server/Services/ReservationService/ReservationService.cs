using BISA.Server.Data.DbContexts;
using BISA.Shared.Constants;
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
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }

        public async Task<LoanReservationEntity> AddReservation(int itemId)
        {
            var userIdFromToken = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userInDb = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userIdFromToken);

            if (userInDb == null)
            {
                throw new ArgumentException("No matching user");
            }

            var item = await _context.Items.FirstOrDefaultAsync(i => i.Id == itemId);

            if (item == null)
            {
                throw new ArgumentException("No item with matching id");
            }

            var duplicateCheck = await _context.LoansReservation
                    .Where(lr => lr.ItemId == itemId && lr.UserId == userInDb.Id)
                    .FirstOrDefaultAsync();

            if (duplicateCheck != null)
            {
                throw new ArgumentException("This item is allready reserved by user");
            }

            var time = CheckTimeAvailable(item.Id);

            var newReservation = new LoanReservationEntity
            {
                UserId = userInDb.Id,
                Date_From = time,
                Date_To = time.AddDays(GetItemLoanTime(item.Type)),
                ItemId = item.Id
            };

            _context.LoansReservation.Add(newReservation);
            await _context.SaveChangesAsync();

            return newReservation;
        }

        public async Task<List<LoanReservationEntity>> GetItemReservations(int itemId)
        {
            var item = await _context.Items.FirstOrDefaultAsync(item => item.Id == itemId);

            if (item == null)
            {
                throw new ArgumentException("No item with matching id");
            }

            return await _context.LoansReservation
                .Where(lr => lr.ItemId == itemId)
                .ToListAsync();
        }

        public async Task<List<LoanReservationEntity>> GetMyReservations()
        {

            var userIdFromToken = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userInDb = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userIdFromToken);

            if (userInDb == null)
            {
                throw new ArgumentException("No matching user");
            }

            return await _context.LoansReservation
                .Include(lrItem => lrItem.Item)
                .Where(lr => lr.UserId == userInDb.Id)
                .ToListAsync();
        }

        public async Task RemoveReservation(int reservationsId)
        {
            var userIdFromToken = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userInDb = await _context.Users.FirstOrDefaultAsync(user => user.UserId == userIdFromToken);

            if (userInDb == null)
            {
                throw new ArgumentException("No matching user");
            }

            var reservationToRemove = await _context.LoansReservation
                .Where(lr => lr.Id == reservationsId)
                .FirstOrDefaultAsync();

            if (reservationToRemove == null)
            {
                throw new NotFoundException("No reservation with matching id");
            }

            if (userInDb.Id != reservationToRemove.UserId)
            {
                throw new UnauthorizedAccessException("User does not have the right to delete this item");
            }

            _context.LoansReservation.Remove(reservationToRemove);
            await _context.SaveChangesAsync();            
        }

        public DateTime CheckTimeAvailable(int id)
        {
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
            }
            else
            {
                return loans[0].Date_To.AddDays(1);
            }
        }

        public double GetItemLoanTime(string itemType) => itemType switch
        {
            "Ebook" => BussinessRulesConstants.EbookLoanTime,
            "Movie" => BussinessRulesConstants.MovieLoanTime,
            _ => BussinessRulesConstants.BookLoanTime
        };

    }
}
