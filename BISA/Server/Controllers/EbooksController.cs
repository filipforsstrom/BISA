using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EbooksController : ControllerBase
    {
        // GET api/<EbooksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int itemId)
        {
            await Task.Delay(1);
            var ebookResponse = new ServiceResponseDTO<EbookDTO>();

            if (ebookResponse.Success)
            {
                return Ok(ebookResponse.Data);
            }
            else
            {
                return BadRequest(ebookResponse.Message);
            }
        }

        // POST api/<EbooksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EbookDTO ebookToAdd)
        {
            await Task.Delay(1);
            var ebookResponse = new ServiceResponseDTO<string>();

            if (ebookResponse.Success)
            {
                return Ok(ebookResponse.Message);
            }
            else
            {
                return BadRequest(ebookResponse.Message);
            }
        }

        // PUT api/<EbooksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] EbookDTO ebookToUpdate)
        {
            await Task.Delay(1);
            var ebookResponse = new ServiceResponseDTO<string>();

            if (ebookResponse.Success)
            {
                return Ok(ebookResponse.Message);
            }
            else
            {
                return BadRequest(ebookResponse.Message);
            }
        }
    }
}
