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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) // string email or user id?
        {
            var userResponse = new ServiceResponseDTO<string>(); // UserViewModel?
            if (userResponse.Success)
            {
                return Ok(userResponse.Data);
            }
            return BadRequest();
        }

        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDTO userChangePassword)
        {
            var changePasswordResponse = await _userService.ChangePassword(userChangePassword);

            if (changePasswordResponse.Success)
            {
                return Ok(changePasswordResponse.Message);
            }
            else
            {
                return BadRequest(changePasswordResponse.Message);
            }

        }
    }
}
