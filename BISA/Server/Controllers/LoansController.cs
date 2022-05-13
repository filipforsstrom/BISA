using BISA.Server.Services.LoanService;
using BISA.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoansController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        // GET: api/<LoanController>
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

        // GET api/<LoanController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var loanResponse = await _loanService.GetMyLoans(id);
            
            if (loanResponse.Success)
            {
                return Ok(loanResponse.Data);
            }
            return BadRequest(loanResponse.Message);
        }

        // POST api/<LoanController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<ItemDTO> loanItems)
        {
            var loanResponse = await _loanService.AddLoan(loanItems);

            if (loanResponse.Success)
            {
                return Created("/loans", loanResponse.Data); 
            }
            return BadRequest(loanResponse.Message);
        }

       

        // DELETE api/<LoanController>/5
        // Bok återlämnad
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Ta bort lån, flytta till lånehistorik, flytta reservationer
            var loanResponse = await _loanService.ReturnLoan(id);
            
            if (loanResponse.Success)
            {
                return NoContent();
            }
            return BadRequest(loanResponse.Message);
        }
    }
}
