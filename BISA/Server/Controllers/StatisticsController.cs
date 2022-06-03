using BISA.Server.Services.StatisticsService;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin, Staff")]
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
            try
            {
                var statResponse = await _statisticsService.GetMostPopularItem();
                return Ok(statResponse);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpGet("users")]
        public async Task<IActionResult> GetMostActiveUser()
        {
            try
            {
                var statResponse = await _statisticsService.GetMostActiveUser();
                return Ok(statResponse);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpGet("authors")]
        public async Task<IActionResult> GetMostPopularAuthor()
        {
            try
            {
                var statResponse = await _statisticsService.GetMostPopularAuthor();
                return Ok(statResponse);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
