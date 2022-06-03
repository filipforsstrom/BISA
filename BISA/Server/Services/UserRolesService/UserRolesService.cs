using BISA.Server.Data.DbContexts;

namespace BISA.Server.Services.UserRolesService
{
    public class UserRolesService : IUserRolesService
    {
        private readonly UserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRolesService(UserDbContext context, UserManager<ApplicationUser> userManager, 
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task<string> DemoteAdmin(string id)
        {
            var userFromContextId = _httpContextAccessor.HttpContext?
                .User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.Equals(userFromContextId, id))
            {
                throw new InvalidOperationException("You can't demote yourself");
            }

            var roleToRemove = "Admin";

            //Find user to demote
            var userToDemote = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            //Check that user exists
            if (userToDemote == null)
            {
                throw new NotFoundException("User could not be found.");
            }

            var userCurrentRoles = await _context.UserRoles.Where(u => u.UserId == id).ToListAsync();

            if (!userCurrentRoles.Any(u => u.RoleId == "AdminId"))
            {
                throw new InvalidOperationException("User is already not admin");
            }

            await RemoveRoles(userToDemote);
            await PromoteToStaff(new UserRoleDTO { Id = id });
            return $"{userToDemote.UserName} demoted from {roleToRemove}.";

        }

        public async Task<string> DemoteStaff(string id)
        {
            var userFromContextId = _httpContextAccessor.HttpContext?
                .User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.Equals(userFromContextId, id))
            {
                throw new InvalidOperationException("You can't demote yourself");
            }
            var roleToRemove = "Staff";

            //Find user to demote
            var userToDemote = await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            //Check that user exists
            if (userToDemote == null)
            {
                throw new NotFoundException("User could not be found.");
            }

            var userCurrentRoles = await _context.UserRoles.Where(u => u.UserId == id).ToListAsync();

            if (!userCurrentRoles.Any(u => u.RoleId == "StaffId"))
            {
                throw new InvalidOperationException("User is already not staff.");
            }

            await RemoveRoles(userToDemote);
            return $"{userToDemote.UserName} demoted from {roleToRemove}.";
        }

        public async Task<string> PromoteToAdmin(UserRoleDTO user)
        {
            var newRole = "Admin";

            //Find user to promote
            var userToPromote = await _context.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync();

            //Check that user exists
            if (userToPromote == null)
            {
                throw new NotFoundException("User could not be found.");
            }

            //Check if user already has role
            var userCurrentRoles = await _context.UserRoles.Where(u => u.UserId == user.Id).ToListAsync();

            if(userCurrentRoles.Any(u => u.RoleId == "AdminId"))
            {
                throw new InvalidOperationException("User is already admin");
            }

            if (userCurrentRoles.Any())
            {
                await RemoveRoles(userToPromote);
            }

            //If user does not have current role, give them role
            await _userManager.AddToRoleAsync(userToPromote, newRole);
            return $"{userToPromote.UserName} promoted to {newRole}.";
        }

        public async Task<string> PromoteToStaff(UserRoleDTO user)
        {
            var newRole = "Staff";

            //Find user to promote
            var userToPromote = await _context.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync();

            //Check that user exists
            if(userToPromote == null)
            {
                throw new NotFoundException("User could not be found.");
                
            }

            //Check if user already has role
            var userCurrentRoles = await _context.UserRoles.Where(u => u.UserId == user.Id).ToListAsync();

            if (userCurrentRoles.Any(u => u.RoleId == "AdminId"))
            {
                throw new InvalidOperationException("Protected user. Contact Administrator");
                
            }
            if (userCurrentRoles.Any(u => u.RoleId == "StaffId"))
            {
                throw new InvalidOperationException("User is already staff");
            }

            if(userCurrentRoles.Any())
            {
                await RemoveRoles(userToPromote);
            }

            //If user does not have current role, give them role
            await _userManager.AddToRoleAsync(userToPromote, newRole);
            return $"{userToPromote.UserName} promoted to {newRole}.";
        }

        private async Task RemoveRoles(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

        } 
    }
}
