using BISA.Server.Data.DbContexts;

namespace BISA.Server.Services.UserRolesService
{
    public class UserRolesService : IUserRolesService
    {
        private readonly UserDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRolesService(UserDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<ServiceResponseDTO<string>> DemoteAdmin(UserRoleDTO user)
        {
            ServiceResponseDTO<string> responseDTO = new();
            var roleToRemove = "Admin";

            //Find user to demote
            var userToDemote = await _context.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync();

            //Check that user exists
            if (userToDemote == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "User could not be found.";
                return responseDTO;
            }

            var userCurrentRoles = await _context.UserRoles.Where(u => u.UserId == user.Id).ToListAsync();

            if (!userCurrentRoles.Any(u => u.RoleId == "AdminId"))
            {
                responseDTO.Success = false;
                responseDTO.Message = "User is already not admin";
                return responseDTO;
            }

            await RemoveRoles(userToDemote);
            responseDTO.Success = true;
            responseDTO.Message = $"{userToDemote.UserName} demoted from {roleToRemove}.";
            return responseDTO;

        }

        public async Task<ServiceResponseDTO<string>> DemoteStaff(UserRoleDTO user)
        {
            ServiceResponseDTO<string> responseDTO = new();
            var roleToRemove = "Staff";

            //Find user to demote
            var userToDemote = await _context.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync();

            //Check that user exists
            if (userToDemote == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "User could not be found.";
                return responseDTO;
            }

            var userCurrentRoles = await _context.UserRoles.Where(u => u.UserId == user.Id).ToListAsync();

            if (!userCurrentRoles.Any(u => u.RoleId == "StaffId"))
            {
                responseDTO.Success = false;
                responseDTO.Message = "User is already not staff.";
                return responseDTO;
            }

            await RemoveRoles(userToDemote);
            responseDTO.Success = true;
            responseDTO.Message = $"{userToDemote.UserName} demoted from {roleToRemove}.";
            return responseDTO;
        }

        public async Task<ServiceResponseDTO<string>> PromoteToAdmin(UserRoleDTO user)
        {

            ServiceResponseDTO<string> responseDTO = new();
            var newRole = "Admin";

            //Find user to promote
            var userToPromote = await _context.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync();

            //Check that user exists
            if (userToPromote == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "User could not be found.";
                return responseDTO;
            }

            //Check if user already has role
            var userCurrentRoles = await _context.UserRoles.Where(u => u.UserId == user.Id).ToListAsync();

            if(userCurrentRoles.Any(u => u.RoleId == "AdminId"))
            {
                responseDTO.Success = false;
                responseDTO.Message = "User is already admin";
                return responseDTO;
            }

            if (userCurrentRoles.Any())
            {
                await RemoveRoles(userToPromote);
            }


            //If user does not have current role, give them role
            await _userManager.AddToRoleAsync(userToPromote, newRole);
            responseDTO.Success = true;
            responseDTO.Message = $"{userToPromote.UserName} promoted to {newRole}.";
            return responseDTO;
        }

        public async Task<ServiceResponseDTO<string>> PromoteToStaff(UserRoleDTO user)
        {
            ServiceResponseDTO<string> responseDTO = new();
            var newRole = "Staff";

            //Find user to promote
            var userToPromote = await _context.Users.Where(u => u.Id == user.Id).FirstOrDefaultAsync();

            //Check that user exists
            if(userToPromote == null)
            {
                responseDTO.Success = false;
                responseDTO.Message = "User could not be found.";
                return responseDTO;
            }

            //Check if user already has role
            var userCurrentRoles = await _context.UserRoles.Where(u => u.UserId == user.Id).ToListAsync();
            

            if (userCurrentRoles.Any(u => u.RoleId == "StaffId"))
            {
                responseDTO.Success = false;
                responseDTO.Message = "User is already staff";
                return responseDTO;
            }

            if(userCurrentRoles.Any())
            {
                await RemoveRoles(userToPromote);
            }

            //If user does not have current role, give them role
            await _userManager.AddToRoleAsync(userToPromote, newRole);
            responseDTO.Success=true;
            responseDTO.Message = $"{userToPromote.UserName} promoted to {newRole}.";
            return responseDTO;
        }

        private async Task RemoveRoles(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

        } 
    }
}
