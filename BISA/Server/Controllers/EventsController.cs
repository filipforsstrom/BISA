using BISA.Server.Services.EventService;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var eventResponse = await _eventService.GetEvents();

            if (eventResponse.Success)
            {
                return Ok(eventResponse.Data);
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }
        }

        [HttpGet("Type")]
        public async Task<IActionResult> GetEventTypes()
        {
            var eventTypeResponse = await _eventService.GetEventTypes();

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
            var eventResponse = await _eventService.GetEvent(id);

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
        public async Task<IActionResult> Post([FromBody] EventDTO eventToCreate)
        {
            //Föreställer mig att datum och tiden man sätter är "2022-05-05 17:00 och inte på millisekunden"
            //eventToCreate.Date = new DateTime(2020, 03, 22);

            //await Task.Delay(1);
            

            var eventResponse = await _eventService.CreateEvent(eventToCreate);

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
            var eventResponse = await _eventService.DeleteEvent(id);

            if (eventResponse.Success)
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
