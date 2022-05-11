using BISA.Server.Data.DbContexts;
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

        public async Task<ServiceResponseDTO<List<LoanDTO>>> AddLoan(List<ItemDTO> items)
        {
            int maxLoans = 5;
            var response = new ServiceResponseDTO<List<LoanDTO>>();

            // get user id using context
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            // simulated user
            var simUser = await _context.Users.FirstOrDefaultAsync();

            var currentUserLoans = await _context.LoansActive
                .Where(l => l.UserId == simUser.Id)
                .ToListAsync();

            if (maxLoans - currentUserLoans.Count > items.Count)
            {
                var loansToAdd = new List<LoanEntity>();

                foreach (var item in items)
                {
                    var freeInvItem = await _context.ItemInventory
                        .Where(i => i.ItemId == item.Id)
                        .FirstOrDefaultAsync(it => it.Available);

                    if (freeInvItem != null)
                    {
                        loansToAdd.Add(new LoanEntity
                        {
                            UserId = simUser.Id,
                            Date_From = DateTime.Now,
                            Date_To = DateTime.Now.AddDays(20),
                            Date_Returned = DateTime.MinValue,
                            ItemInventoryId = freeInvItem.Id,
                            ItemInventory = freeInvItem,
                            User = simUser,
                        });

                        freeInvItem.Available = false;
                        _context.ItemInventory.Update(freeInvItem);
                        await _context.SaveChangesAsync();
                    }
                }

                if (loansToAdd.Any())
                {
                    _context.LoansActive.AddRange(loansToAdd);
                    await _context.SaveChangesAsync();

                    response.Data = ConvertToDTO(loansToAdd);
                    response.Success = true;
                    return response;
                }

                response.Success = false;
                response.Message = "Could not add loans to user";
                return response;
            }
            response.Success = false;
            response.Message = $"You're only eligible for {maxLoans - currentUserLoans.Count} more loans";
            return response;
        }

        public async Task<ServiceResponseDTO<List<LoanDTO>>> GetAllLoans()
        {
            var response = new ServiceResponseDTO<List<LoanDTO>>();

            var loans = await _context.LoansActive
                .Include(l => l.User)
                .Include(l => l.ItemInventory)
                .ToListAsync();

            if (loans != null)
            {
                response.Data = ConvertToDTO(loans);
                response.Success = true;
                return response;
            }
            response.Success = false;
            response.Message = "No loans in database";
            return response;
        }

        public async Task<ServiceResponseDTO<List<LoanDTO>>> GetMyLoans(int id)
        {
            var response = new ServiceResponseDTO<List<LoanDTO>>();
            // get user from context, simulated by id

            // get matching user from db
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                var userLoans = await _context.LoansActive
                    .Include(l => l.ItemInventory)
                    .Include(l => l.User)
                    .Where(l => l.UserId == id)
                    .ToListAsync();

                if (userLoans != null)
                {
                    response.Data = ConvertToDTO(userLoans);
                    response.Success = true;
                    return response;
                }
                response.Success = false;
                response.Message = "No active loans";
                return response;

            }
            response.Success = false;
            response.Message = "No matching user found";
            return response;
        }

        public async Task<ServiceResponseDTO<string>> ReturnLoan(int id)
        {
            var response = new ServiceResponseDTO<string>();

            var loanToRemove = await _context.LoansActive
                .FirstOrDefaultAsync(l => l.Id == id);

            if (loanToRemove != null)
            {
                // remove loan
                _context.LoansActive.Remove(loanToRemove);
                await _context.SaveChangesAsync();

                // toggle available
                var invItem = await _context.ItemInventory
                    .FirstOrDefaultAsync(i => i.Id == loanToRemove.ItemInventoryId);

                invItem.Available = true;
                _context.Update(invItem);
                await _context.SaveChangesAsync();

                response.Success = true;
                return response;
            }
            response.Success = false;
            response.Message = "No matching loan found";
            return response;
        }

        private List<LoanDTO>? ConvertToDTO(List<LoanEntity> loans)
        {
            var result = new List<LoanDTO>();

            foreach (var loan in loans)
            {
                result.Add(new LoanDTO
                {
                    Id = loan.Id,
                    Date_From = loan.Date_From,
                    Date_To = loan.Date_To,
                    User_Email = loan.User?.Email,
                    ItemId = loan.ItemInventory.ItemId,
                    InvItemId = loan.ItemInventoryId
                });
            }
            return result;
        }
    }
}
