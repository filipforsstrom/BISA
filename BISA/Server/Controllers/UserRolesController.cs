using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {
        // GET: api/<UserRolesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<UserRolesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserRolesController>
        [HttpPost("{id}")]
        public async Task<IActionResult> PromoteToStaff(int id) //bara make staff? heta bara post?
        {
            var eventResponse = new ServiceResponseDTO<string>();

            if (eventResponse.Success)
            {
                return Ok(eventResponse.Message);
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }

        }

        [HttpPost("/[action]/{id}")]
        public async Task<IActionResult> PromoteToAdmin(int id) //bara make staff? 
        {
            var eventResponse = new ServiceResponseDTO<string>();

            if (eventResponse.Success)
            {
                return Ok(eventResponse.Message);
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }

        }

        // PUT api/<UserRolesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<UserRolesController>/5
        [HttpDelete("/demoteAdmin/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eventResponse = new ServiceResponseDTO<string>();

            if (eventResponse.Success)
            {
                return Ok(eventResponse.Message);
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }

        }
        // DELETE api/<UserRolesController>/5
        [HttpDelete("/demoteStaff/{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            var eventResponse = new ServiceResponseDTO<string>();

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
