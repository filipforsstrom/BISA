using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UsersController>/5
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

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDTO userChangePassword)
        {
            var changePasswordResponse = new ServiceResponseDTO<UserChangePasswordDTO>(); 

            if(changePasswordResponse.Success)
            {
                return Ok(changePasswordResponse.Message);
            }
            else
            {
                return BadRequest(changePasswordResponse.Message);
            }

        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userResponse = new ServiceResponseDTO<string>();
            if (userResponse.Success)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
