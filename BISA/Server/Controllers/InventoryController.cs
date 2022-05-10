using BISA.Server.Services.InventoryService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BISA.Server.Controllers
{
    [Route("api/Inventory")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

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


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemInventoryDTO itemInventoryAdd)
        {
            var inventoryResponse = await _inventoryService.AddItemInventory(itemInventoryAdd);

            if (inventoryResponse.Success)
            {
                return Ok(inventoryResponse.Data);
            }
            else
            {
                return BadRequest(inventoryResponse.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, ItemInventoryDTO itemInventoryDelete)
        {
            itemInventoryDelete.InventoryId = id;
            var inventoryResponse = await _inventoryService.DeleteItemInventory(itemInventoryDelete);

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
