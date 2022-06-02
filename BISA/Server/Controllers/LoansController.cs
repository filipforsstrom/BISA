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
            try
            {
                var loanResponse = await _loanService.GetAllLoans();
                return Ok(loanResponse);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest("Error calling the database");
            }
            
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserLoans()
        {

            try
            {
                var loanResponse = await _loanService.GetMyLoans();
                return Ok(loanResponse);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch(InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
            catch(Exception exception)
            {
                return BadRequest();
            }
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
