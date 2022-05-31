using BISA.Server.Services.LoanService;
using BISA.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var loanResponse = await _loanService.GetAllLoans();

            if (loanResponse.Success)
            {
                return Ok(loanResponse.Data);
            }
            return BadRequest(loanResponse.Message);
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserLoans()
        {
            var loanResponse = await _loanService.GetMyLoans();

            if (loanResponse.Success)
            {
                return Ok(loanResponse.Data);
            }
            return BadRequest(loanResponse.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<CheckoutDTO> loanItems)
        {
            var loanResponse = await _loanService.AddLoan(loanItems);

            if (loanResponse.Success)
            {
                return Created("/loans", loanResponse.Data);
            }
            return BadRequest(loanResponse.Message);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var loanResponse = await _loanService.ReturnLoan(id);

            if (loanResponse.Success)
            {
                return NoContent();
            }
            return BadRequest(loanResponse.Message);
        }
    }
}
