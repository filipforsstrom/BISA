using BISA.Server.Services.ItemService;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService ItemService)
        {
            _itemService = ItemService;
        }

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
            var itemResponse = new ServiceResponseDTO<ItemDTO>();

            var itemResponsen = _itemService.GetItem(id);

            if (itemResponse.Success)
            {
                return Ok(itemResponse.Data);
            }
            else
            {
                return BadRequest(itemResponse.Message);
            }

        }

        // POST api/<ItemsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] string AddItemDTO) // här kommer AddItemDTO in
        {
            var itemResponse = new ServiceResponseDTO<string>();

            if (itemResponse.Success)
            {
                return Created("", itemResponse); // vad vill vi returnera med created? message eller created
            }
            else
            {
                return BadRequest(itemResponse.Message);
            }

        }

        // PUT api/<ItemsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string AddItemDTO) // här kommer AddItemDTO in, ska vi ha ett helt objekt in?
        {
            var itemResponse = new ServiceResponseDTO<string>();

            if (itemResponse.Success)
            {
                return Ok(itemResponse.Message);
            }
            else
            {
                return BadRequest(itemResponse.Message);
            }

        }

        // DELETE api/<ItemsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) // skall vi rensa inventory här eller ska den vara en egen controller? 
        {


        }
    }
}
