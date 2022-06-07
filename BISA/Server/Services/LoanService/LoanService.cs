using BISA.Server.Data.DbContexts;
using BISA.Shared.Constants;
using BISA.Shared.Entities;

namespace BISA.Server.Services.LoanService
{
    public class LoanService : ILoanService
    {
        private readonly BisaDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoanService(BisaDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<List<LoanDTO>> AddLoan(List<CheckoutDTO> items)
        {

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userInDb = await _context.Users
                .FirstOrDefaultAsync(user => user.UserId == userId);

            if (userInDb == null)
            {
                throw new UserNotFoundException("User not found.");
            }

            var currentUserLoans = await _context.LoansActive
                .Where(l => l.UserId == userInDb.Id)
                .ToListAsync();

            if (BussinessRulesConstants.MaxLoansPerUser - currentUserLoans.Count >= items.Count)
            {
                string infoMessage = "Following items could not be loaned:";
                var loansToAdd = new List<LoanEntity>();

                foreach (var item in items)
                {
                    var freeInvItem = await _context.ItemInventory
                        .Include(i => i.Item)
                        .Where(i => i.ItemId == item.ItemId)
                        .FirstOrDefaultAsync(it => it.Available);

                    if (freeInvItem != null)
                    {
                        loansToAdd.Add(new LoanEntity
                        {
                            UserId = userInDb.Id,
                            Date_From = DateTime.Now,
                            Date_To = DateTime.Now.AddDays(GetItemLoanTime(freeInvItem.Item.Type)),
                            ItemInventoryId = freeInvItem.Id,
                        });

                        freeInvItem.Available = false;
                        _context.ItemInventory.Update(freeInvItem);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        infoMessage = $"{infoMessage} {item.Title}";
                    }
                }

                if (loansToAdd.Any())
                {
                    _context.LoansActive.AddRange(loansToAdd);
                    await _context.SaveChangesAsync();

                    var addedLoans = ConvertToDTO(loansToAdd);
                    return addedLoans;
                }

                throw new InvalidOperationException(infoMessage);
            }

            throw new ArgumentOutOfRangeException($"User only eligible for {BussinessRulesConstants.MaxLoansPerUser - currentUserLoans.Count} more loans");
        }

        public async Task<List<LoanDTO>> GetAllLoans()
        {
            var loans = await _context.LoansActive
                .Include(l => l.User)
                .Include(l => l.ItemInventory)
                .ThenInclude(inv => inv.Item)
                .ToListAsync();

            if (loans != null)
            {
                var userLoans = ConvertToDTO(loans);
                return userLoans;
            }

            throw new NotFoundException("No loans found");
        }

        public async Task<List<LoanDTO>> GetMyLoans()
        {

            var userIdFromToken = _httpContextAccessor.HttpContext.User
                .FindFirstValue(ClaimTypes.NameIdentifier);

            var userInDb = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userIdFromToken);

            if (userInDb != null)
            {
                var userLoans = await _context.LoansActive
                    .Include(l => l.ItemInventory)
                    .ThenInclude(inv => inv.Item)
                    .Include(l => l.User)
                    .Where(l => l.UserId == userInDb.Id)
                    .ToListAsync();

                if (userLoans.Any())
                {
                    var userLoansDtos = ConvertToDTO(userLoans);
                    return userLoansDtos;
                }

                throw new NotFoundException("You do not have any loans.");

            }

            throw new InvalidOperationException("Error calling the database");
        }

        public async Task<string> ReturnLoan(int id)
        {

            var invItemReturned = await _context.ItemInventory.FirstOrDefaultAsync(i => i.Id == id);

            if (invItemReturned == null)
            {
                throw new NotFoundException("No matching item found.");
            }

            var loanToRemove = await _context.LoansActive
                .Include(loan => loan.ItemInventory)
                .FirstOrDefaultAsync(l => l.ItemInventoryId == invItemReturned.Id);

            if (loanToRemove != null)
            {
                // remove loan
                _context.LoansActive.Remove(loanToRemove);
                await _context.SaveChangesAsync();

                // Check if item has pending reservations
                var checkReservations = await GetFirstItemReservation(loanToRemove.ItemInventory.ItemId);

                if (checkReservations != null)
                {
                    await RemoveReservation(checkReservations.Id);
                    // Move reservation to active loan
                    await MoveReservationToLoan(checkReservations, loanToRemove.ItemInventoryId);
                }
                else
                {
                    // toggle invItem available
                    var invItem = await _context.ItemInventory
                        .FirstOrDefaultAsync(i => i.Id == loanToRemove.ItemInventoryId);

                    invItem.Available = true;
                    _context.Update(invItem);
                    await _context.SaveChangesAsync();
                }

                return "Loan returned";
            }

            throw new NotFoundException("No matching loan found");
        }

        private double GetItemLoanTime(string itemType) => itemType switch
        {
            "Ebook" => BussinessRulesConstants.EbookLoanTime,
            "Movie" => BussinessRulesConstants.MovieLoanTime,
            _ => BussinessRulesConstants.BookLoanTime
        };

        private async Task RemoveReservation(int reservationId)
        {
            var reservationToRemove = await _context.LoansReservation
                .FirstOrDefaultAsync(res => res.Id == reservationId);
            _context.LoansReservation.Remove(reservationToRemove);
            await _context.SaveChangesAsync();
        }

        private async Task MoveReservationToLoan(LoanReservationEntity reservationToMove, int invItemId)
        {
            var newLoan = new LoanEntity
            {
                UserId = reservationToMove.UserId,
                ItemInventoryId = invItemId,
                Date_From = DateTime.Now,
                Date_To = DateTime.Now.AddDays(GetItemLoanTime(reservationToMove.Item.Type))
            };
            _context.LoansActive.Add(newLoan);
            await _context.SaveChangesAsync();
        }

        private async Task<LoanReservationEntity> GetFirstItemReservation(int itemId)
        {
            // ta reservation på plats 1 i kön
            var itemReservation = await _context.LoansReservation
                .Include(reservation => reservation.Item)
                .FirstOrDefaultAsync(lr => lr.ItemId == itemId);

            if (itemReservation != null)
            {
                return itemReservation;
            }
            return null;
        }

        private List<LoanDTO>? ConvertToDTO(IEnumerable<LoanEntity> loans)
        {
            var result = new List<LoanDTO>();

            if (loans != null)
            {
                foreach (var loan in loans)
                {
                    result.Add(new LoanDTO
                    {
                        Date_From = loan.Date_From,
                        Date_To = loan.Date_To,
                        User_Email = loan.User?.Email,
                        Item = loan.ItemInventory.Item,
                        InvItemId = loan.ItemInventoryId
                    });
                }
            }
            return result;
        }
    }
}
