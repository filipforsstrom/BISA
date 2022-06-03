using BISA.Server.Services.UserRolesService;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Staff")]
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
            try
            {
                var eventResponse = await _userRolesService.PromoteToStaff(userToPromote);
                return Ok(eventResponse);
            }
            catch(InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch(NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PromoteToAdmin(UserRoleDTO userToPromote)
        {
            try
            {
                var eventResponse = await _userRolesService.PromoteToAdmin(userToPromote);
                return Ok(eventResponse);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch(NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAdmin(string id)
        {
            try
            {
                var eventResponse = await _userRolesService.DemoteAdmin(id);
                return Ok(eventResponse);
            }
            catch(InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch(NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteStaff(string id)
        {
            
            try
            {
                var eventResponse = await _userRolesService.DemoteStaff(id);
                return Ok(eventResponse);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch(NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
