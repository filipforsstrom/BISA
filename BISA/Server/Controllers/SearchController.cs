using BISA.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        // GET: api/<SearchController>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByTitle(SearchDTO searchParams)
        {
            // Filter response by title, tags
            var searchResponse = new ServiceResponseDTO<List<ItemDTO>>();
            if (searchResponse.Success)
            {
                return Ok(searchResponse.Data);
            }
            return BadRequest(searchResponse.Message);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByTags(SearchDTO searchParams)
        {
            // Filter response by title, tags
            var searchResponse = new ServiceResponseDTO<List<ItemDTO>>();
            if (searchResponse.Success)
            {
                return Ok(searchResponse.Data);
            }
            return BadRequest(searchResponse.Message);
        }
    }
}
