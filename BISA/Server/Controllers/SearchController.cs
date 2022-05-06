using BISA.Server.Services.SearchService;
using BISA.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        // GET: api/<SearchController>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetByTitle([FromQuery] string searchTitle) // searchdto
        {
            if (string.IsNullOrEmpty(searchTitle))
            {
                return BadRequest("Please enter a title");
            }
            var searchResponse = await _searchService.SearchByTitle(searchTitle);
            if (searchResponse.Success)
            {
                return Ok(searchResponse.Data);
            }
            return BadRequest(searchResponse.Message);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetByTags([FromQuery] string tag)
        {
            if (string.IsNullOrEmpty(tag))
            {
                return BadRequest("Please enter a subject");
            }
            // Filter response by title, tags
            var searchResponse = await _searchService.SearchByTags(tag);
            if (searchResponse.Success)
            {
                return Ok(searchResponse.Data);
            }
            return BadRequest(searchResponse.Message);
        }
    }
}
