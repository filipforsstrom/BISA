using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: api/<AuthController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AuthController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param UserLoginDTO="user"></param>
        /// <returns> returns a valid JWT token if serviceResponse is valid </returns>
        /// 
        // POST api/<AuthController>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
        {
            await Task.Delay(1);

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
            await Task.Delay(1);

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

        // PUT api/<AuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
