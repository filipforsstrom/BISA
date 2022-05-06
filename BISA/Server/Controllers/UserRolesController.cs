using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRolesController : ControllerBase
    {

        // POST api/<UserRolesController>
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> PromoteToStaff(int id)
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

        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> PromoteToAdmin(int id)
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
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
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
        [HttpDelete("[action]/{id}")]
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
