using BISA.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // Get reservations for one item
            var resResponse = new ServiceResponseDTO<LoanReservationEntity>();
            if (resResponse.Success)
            {
                return Ok(resResponse.Data);
            }
            return BadRequest(resResponse.Message);
        }

        [HttpGet("/user/{id}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var resResponse = new ServiceResponseDTO<LoanReservationEntity>();
            if (resResponse.Success)
            {
                return Ok(resResponse.Data);
            }
            return BadRequest(resResponse.Message);
        }
    }
}
