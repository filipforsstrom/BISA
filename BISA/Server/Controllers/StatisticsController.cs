using BISA.Server.Services.StatisticsService;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Populäraste item i biblioteket
            var statResponse = await _statisticsService.GetMostPopularItem();

            if (statResponse.Success)
            {
                return Ok(statResponse.Data);
            }
            return BadRequest(statResponse.Message);
        }
    }
}
