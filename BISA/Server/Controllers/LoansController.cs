using BISA.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        // GET: api/<LoanController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var loanResponse = new ServiceResponseDTO<List<LoanEntity>>();
            var loans = new List<LoanEntity>();
            loanResponse.Data = loans;
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
            // Hämta en användares lån
            var loanResponse = new ServiceResponseDTO<List<LoanEntity>>();
            var loans = new List<LoanEntity>();
            loanResponse.Data = loans;
            if (loanResponse.Success)
            {
                return Ok(loanResponse.Data);
            }
            return BadRequest(loanResponse.Message);
        }

        // POST api/<LoanController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<string> loanItems) // Krävs korrekt DTO
        {
            var loanResponse = new ServiceResponseDTO<List<string>>();

            if (loanResponse.Success)
            {
                return Created("/loans", loanResponse.Data); 
            }
            return BadRequest(loanResponse.Message);
        }

        // PUT api/<LoanController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoanController>/5
        // Bok återlämnad
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Ta bort lån, flytta till lånehistorik, flytta reservationer
            var loanResponse = new ServiceResponseDTO<List<LoanEntity>>();
            
            if (loanResponse.Success)
            {
                return NoContent();
            }
            return BadRequest(loanResponse.Message);
        }
    }
}
