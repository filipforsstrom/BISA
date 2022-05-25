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

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var itemsResponse = await _itemService.GetItems();

            if (itemsResponse.Success)
            {
                return Ok(itemsResponse.Data);
            }
            else
            {
                return BadRequest(itemsResponse.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var itemResponse = await _itemService.GetItem(id);

            if (itemResponse.Success)
            {
                return Ok(itemResponse.Data);
            }
            else
            {
                return BadRequest(itemResponse.Message);
            }
        }

        [HttpGet("tags")]
        public async Task<IActionResult> GetTags()
        {
            var tagResponse = await _itemService.GetTags();

            if (tagResponse.Success)
            {
                return Ok(tagResponse.Data);
            }
            else
            {
                return BadRequest(tagResponse.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) // skall vi rensa inventory här eller ska den vara en egen controller? 
        {
            var deleteResponse = await _itemService.DeleteItem(id);

            if (deleteResponse.Success)
            {
                return NotFound(deleteResponse.Message);
            }

            return BadRequest(deleteResponse.Message);
        }
    }
}
