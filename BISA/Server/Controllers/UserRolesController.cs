using BISA.Server.Services.UserRolesService;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRolesService _userRolesService;

        public UserRolesController(IUserRolesService userRolesService)
        {
            _userRolesService = userRolesService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PromoteToStaff(UserRoleDTO userToPromote)
        {

            var eventResponse = await _userRolesService.PromoteToStaff(userToPromote);

            if (eventResponse.Success)
            {
                return Ok(eventResponse.Message);
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PromoteToAdmin(UserRoleDTO userToPromote)
        {
            var eventResponse = await _userRolesService.PromoteToAdmin(userToPromote);

            if (eventResponse.Success)
            {
                return Ok(eventResponse.Message);
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }

        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteAdmin(UserRoleDTO userToDemote)
        {
            var eventResponse = await _userRolesService.DemoteAdmin(userToDemote);

            if (eventResponse.Success)
            {
                return Ok(eventResponse.Message);
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }

        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteStaff(UserRoleDTO userToDemote)
        {
            var eventResponse = await _userRolesService.DemoteStaff(userToDemote);

            if (eventResponse.Success)
            {
                return Ok(eventResponse.Message);
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }

        }
    }
}
