using BISA.Server.Services.InventoryService;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemInventory(int id)
        {
            var inventoryResponse = await _inventoryService.GetItemsInventory(id);

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
        public async Task<IActionResult> Post([FromBody] ItemInventoryChangeDTO itemInventoryAdd)
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
        public async Task<IActionResult> Delete(int id)
        {
            var inventoryResponse = await _inventoryService.DeleteItemInventory(id);

            if (inventoryResponse.Success)
            {
                return NotFound(inventoryResponse.Message);
            }
            else
            {
                return BadRequest(inventoryResponse.Message);
            }
        }
    }
}
