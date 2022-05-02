using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var eventResponse = new ServiceResponseDTO<List<EventDTO>>();

            if(eventResponse.Success)
            {
                return Ok(eventResponse.Data);
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEventTypes()
        {
            var eventTypeResponse = new ServiceResponseDTO<List<EventTypeDTO>>();

            if (eventTypeResponse.Success)
            {
                return Ok(eventTypeResponse.Data);
            }
            else
            {
                return BadRequest(eventTypeResponse.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var eventResponse = new ServiceResponseDTO<EventDTO>();

            if (eventResponse.Success)
            {
                return Ok(eventResponse.Data);
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EventDTO createdEvent)
        {
            await Task.Delay(1);

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EventDTO eventToChange)
        {
            var eventResponse = new ServiceResponseDTO<EventDTO>();

            if (eventResponse.Success)
            {
                return Ok(eventResponse.Data);
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eventResponse = new ServiceResponseDTO<EventDTO>();

            if(eventResponse.Success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }
        }
    }
}
