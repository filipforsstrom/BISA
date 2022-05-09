using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BISA.Server.Controllers
{
    [Route("api/Inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        // GET: api/<InventoriesController>
        [HttpGet]
        public async Task<IActionResult> Get(int itemId)
        {
            var inventoryResponse = new ServiceResponseDTO<List<int>>(); //lista med id´s

            if (inventoryResponse.Success)
            {
                return Ok(inventoryResponse.Data);
            }
            else
            {
                return BadRequest(inventoryResponse.Message);
            }
        }



        // POST api/<InventoriesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] int itemId, int amountOfItems)
        {
            var inventoryResponse = new ServiceResponseDTO<List<int>>();

            if (inventoryResponse.Success)
            {
                return Ok(inventoryResponse.Data);
            }
            else
            {
                return BadRequest(inventoryResponse.Message);
            }

        }



        // DELETE api/<InventoriesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int inventoryId)
        {
            var inventoryResponse = new ServiceResponseDTO<string>();

            if (inventoryResponse.Success)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(inventoryResponse.Message);
            }
        }
    }
}
