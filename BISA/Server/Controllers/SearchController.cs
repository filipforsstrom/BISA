using BISA.Server.Services.SearchService;
using BISA.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("title")]
        public async Task<IActionResult> GetByTitle([FromQuery] string title) // searchdto
        {
            try
            {
                var searchResponse = await _searchService.SearchByTitle(title);
                return Ok(searchResponse);
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

        [HttpGet("tag")]
        public async Task<IActionResult> GetByTags([FromQuery] string tag)
        {
            try
            {
                var searchResponse = await _searchService.SearchByTags(tag);
                return Ok(searchResponse);
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

        [HttpGet("all")]
        public async Task<IActionResult> GetByAll([FromQuery] string search)
        {
            try
            {
                var searchResponse = await _searchService.SearchByAll(search);
                return Ok(searchResponse);
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
    }
}
