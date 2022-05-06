using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        // POST api/<AuthController>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
        {           
            var loginResponse = new ServiceResponseDTO<string>();

            if (loginResponse.Success)
            {
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
            var registerResponse = new ServiceResponseDTO<string>();

            if (registerResponse.Success)
            {
                return Ok(registerResponse.Data);
            }
            else
            {
                return BadRequest(registerResponse.Message);
            }


        }
    }
}
