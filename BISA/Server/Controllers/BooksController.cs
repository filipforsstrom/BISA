using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
       

        // GET api/<BooksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            await Task.Delay(1);
            var bookResponse = new ServiceResponseDTO<BookDTO>();

            if (bookResponse.Success)
            {
                return Ok(bookResponse.Data);
            }
            else
            {
                return BadRequest(bookResponse.Message);
            }
        }

        // POST api/<BooksController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookDTO bookToAdd)
        {
            await Task.Delay(1);
            var bookResponse = new ServiceResponseDTO<string>();

            if (bookResponse.Success)
            {
                return Ok(bookResponse.Message);
            }
            else
            {
                return BadRequest(bookResponse.Message);
            }
        }

        // PUT api/<BooksController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] BookDTO bookToUpdate)
        {
            await Task.Delay(1);
            var bookResponse = new ServiceResponseDTO<string>();

            if (bookResponse.Success)
            {
                return Ok(bookResponse.Message);
            }
            else
            {
                return BadRequest(bookResponse.Message);
            }
        }

       
    }
}
