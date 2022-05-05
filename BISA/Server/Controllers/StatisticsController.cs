using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // Populäraste item i biblioteket
            var statResponse = new ServiceResponseDTO<ItemDTO>();
            if (statResponse.Success)
            {
                return Ok(statResponse.Data);
            }
            return BadRequest(statResponse.Message);
        }
    }
}
