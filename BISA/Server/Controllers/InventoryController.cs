using BISA.Server.Services.InventoryService;
using Microsoft.AspNetCore.Mvc;

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


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ItemInventoryDTO itemInventoryAdd)
        {
            var inventoryResponse = await _inventoryService.AddItemInventory(itemInventoryAdd);

            if (inventoryResponse.Success)
            {
                return Ok(inventoryResponse.Message);
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
