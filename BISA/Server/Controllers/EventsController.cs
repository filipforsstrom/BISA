using BISA.Server.Services.EventService;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/events")]
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
            try
            {
                var eventResponse = await _eventService.GetEvents();
                return Ok(eventResponse);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpGet("types")]
        public async Task<IActionResult> GetEventTypes()
        {   
            try
            {
                var eventResponse = await _eventService.GetEventTypes();
                return Ok(eventResponse);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            
            try
            {
                var eventResponse = await _eventService.GetEvent(id);
                return Ok(eventResponse);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch(Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Post([FromBody] EventCreateDTO eventToCreate)
        {
            try
            {
                var eventResponse = await _eventService.CreateEvent(eventToCreate);
                return Ok(eventResponse);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }

            catch (DbUpdateException exception)
            {
                return StatusCode(500, exception.Message);
            }
            


        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Put(int id, [FromBody] EventDTO eventToUpdate)
        {
            try
            {
                var eventResponse = await _eventService.UpdateEvent(id, eventToUpdate);
                return Ok(eventResponse);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }


        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Delete(int id)
        {
            
            try
            {
                var eventResponse = await _eventService.DeleteEvent(id);
                return NoContent();
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
             
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }

        }
    }
}
