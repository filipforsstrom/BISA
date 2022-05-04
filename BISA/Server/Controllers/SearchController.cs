using BISA.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        // GET: api/<SearchController>
        [HttpGet]
        public async Task<IActionResult> Get(SearchDTO searchParams)
        {
            // Filter response by title, tags
            var searchResponse = new ServiceResponseDTO<List<ItemDTO>>();
            if (searchResponse.Success)
            {
                return Ok(searchResponse.Data);
            }
            return BadRequest(searchResponse.Message);
        }

        // GET api/<SearchController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

    }
}
