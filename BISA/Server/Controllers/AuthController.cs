using BISA.Server.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

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
            var loginResponse = await _authService.Login(user);

            if (loginResponse.Success)
            {
                HttpContext.Response.Headers.Add("X-AuthToken", loginResponse.Data); // for RestClient in vscode
                return Ok(loginResponse.Data);
            }
            else
            {
                return BadRequest(loginResponse.Message);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
        {
            var registerResponse = await _authService.Register(user);

            if (registerResponse.Success)
            {
                HttpContext.Response.Headers.Add("X-AuthToken", registerResponse.Data); // for RestClient in vscode
                return Ok(registerResponse);
            }
            else
            {
                return BadRequest(registerResponse.Message);
            }
        }
    }
}
