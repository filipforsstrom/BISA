using BISA.Server.Services.StatisticsService;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        [HttpGet("popular")]
        public async Task<IActionResult> GetMostPopularItem()
        {
            // Populäraste item i biblioteket
            var statResponse = await _statisticsService.GetMostPopularItem();

            if (statResponse.Success)
            {
                return Ok(statResponse.Data);
            }
            return BadRequest(statResponse.Message);
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetMostActiveUser()
        {

            var statResponse = await _statisticsService.GetMostActiveUser();

            if (statResponse.Success)
            {
                return Ok(statResponse.Data);
            }
            return BadRequest(statResponse.Message);
        }

        [HttpGet("authors")]
        public async Task<IActionResult> GetMostPopularAuthor()
        {
            var statResponse = await _statisticsService.GetMostPopularAuthor();

            if (statResponse.Success)
            {
                return Ok(statResponse.Data);
            }
            return BadRequest(statResponse.Message);
        }
    }
}
