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
            try
            {
                var itemsResponse = await _itemService.GetItems();
                return Ok(itemsResponse);
            }
            catch(NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
             try
             {
                 var itemResponse = await _itemService.GetItem(id);
                 return Ok(itemResponse);
             }
             catch (NotFoundException exception)
             {
                 return NotFound(exception.Message);
             }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("tags")]
        public async Task<IActionResult> GetTags()
        {
            try
            {
                var tagResponse = await _itemService.GetTags();
                return Ok(tagResponse);
            }
            catch(NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Delete(int id) // skall vi rensa inventory här eller ska den vara en egen controller? 
        {
     
            try
            {
                var deleteResponse = await _itemService.DeleteItem(id);
                return NoContent();
            }
            catch(InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch(NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
