using BISA.Server.Services.AuthService;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;

namespace BISA.Server.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
        {
            try
            {
                var loginResponse = await _authService.Login(user);
                HttpContext.Response.Headers.Add("X-AuthToken", loginResponse); // for RestClient in vscode
                return Ok(loginResponse);
            }
            catch (AuthenticationException exception)
            {
                return Unauthorized(exception.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
        {
            try
            {
                var registerResponse = await _authService.Register(user);
                return Ok(registerResponse);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (AuthenticationException exception)
            {
                return Unauthorized(exception.Message);
            }
            catch (DbUpdateException exception)
            {
                return StatusCode(500, exception.Message);
            }
        }
    }
}
