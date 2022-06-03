using BISA.Server.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        [Authorize(Roles = "Admin, Staff")]
        [HttpGet("{username}")]
        public async Task<IActionResult> Get(string username)
        {
            try
            {
                var userResponse = await _userService.GetUser(username);
                return Ok(userResponse);
            }
            catch(ArgumentException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [Authorize]
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDTO userChangePassword)
        {
            try
            {
                var changePasswordResponse = await _userService.ChangePassword(userChangePassword);
                return Ok(changePasswordResponse);
            }
            catch (ApplicationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }         
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return NoContent();
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
