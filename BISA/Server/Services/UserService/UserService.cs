using BISA.Server.Data.DbContexts;
using BISA.Shared.Entities;

namespace BISA.Server.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly BisaDbContext _context;

        public UserService(IHttpContextAccessor httpContextAccessor, SignInManager<ApplicationUser> signInManager, BisaDbContext context)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _context = context;
        }
        public async Task<string> ChangePassword(UserChangePasswordDTO userChangePassword)
        {            
            var userId = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _signInManager.UserManager.FindByIdAsync(userId);

            if (user == null)
            {
                throw new ApplicationException("User not found.");
            }

            var changePasswordResult = await _signInManager.UserManager.ChangePasswordAsync(
                user, userChangePassword.CurrentPassword, userChangePassword.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                throw new ApplicationException(changePasswordResult.Errors.First().Description.ToString());
            }

            return "Password successfully changed";
        }

        public async Task DeleteUser(string id)
        {
            var userInUserDb = await _signInManager.UserManager.FindByIdAsync(id);

            if (userInUserDb == null)
            {
                throw new ArgumentException("User not found");
            }

            await _signInManager.UserManager.DeleteAsync(userInUserDb);
            
            var userInBisaDb = await _context.Users.FirstOrDefaultAsync(user => user.UserId == id);
            if (userInBisaDb != null)
            {
                _context.Users.Remove(userInBisaDb);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<UserRoleDTO> GetUser(string username)
        {
            var userInDb = await _signInManager.UserManager.FindByNameAsync(username);

            if (userInDb == null)
            {
                throw new ArgumentException("No matching user");
            }

            return new UserRoleDTO
            {
                Id = userInDb.Id,
                Email = userInDb.Email,
                Username = userInDb.UserName
            };
        }
    }
}
