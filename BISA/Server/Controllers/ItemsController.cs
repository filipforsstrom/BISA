using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {


        /*
         * Get one item
         * Add Item
         * Update item
         * Delete iteminventory? 
         
         
         */



        // GET: api/<ItemsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ItemsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var eventResponse = new ServiceResponseDTO<ItemDTO>();

            if (eventResponse.Success)
            {
                return Ok(eventResponse.Data);
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }

        }

        // POST api/<ItemsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string AddItemDTO) // här kommer AddItemDTO in
        {
            var eventResponse = new ServiceResponseDTO<string>();

            if (eventResponse.Success)
            {
                return Created("", eventResponse); // vad vill vi returnera med created? message eller created
            }
            else
            {
                return BadRequest(eventResponse.Message);
            }

        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string AddItemDTO) // här kommer AddItemDTO in, ska vi ha ett helt objekt in?
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

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) // skall vi rensa inventory här eller ska den vara en egen controller? 
        {


        }
    }
}
