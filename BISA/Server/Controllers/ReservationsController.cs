using BISA.Server.Services.LoanService;
using BISA.Server.Services.ReservationService;
using BISA.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            // Get reservations for one item
            var resResponse = await _reservationService.GetItemReservations(id);
            if (resResponse.Success)
            {
                return Ok(resResponse.Data);
            }
            return BadRequest(resResponse.Message);
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetUserReservations()
        {
            var resResponse = await _reservationService.GetMyReservations();
            if (resResponse.Success)
            {
                return Ok(resResponse.Data);
            }
            return BadRequest(resResponse.Message);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> Post(int id)
        {
            var resResponse = await _reservationService.AddReservation(id);
            if (resResponse.Success)
            {
                return Ok(resResponse.Data);
            }
            return BadRequest(resResponse.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var resResponse = await _reservationService.RemoveReservation(id);
            if (resResponse.Success)
            {
                return NoContent();
            }
            return BadRequest(resResponse.Message);
        }
    }
}
