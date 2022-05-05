using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int itemId)
        {
            await Task.Delay(1);
            var movieResponse = new ServiceResponseDTO<MovieDTO>();

            if (movieResponse.Success)
            {
                return Ok(movieResponse.Data);
            }
            else
            {
                return BadRequest(movieResponse.Message);
            }
        }

        // POST api/<MoviesController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieDTO movieToAdd)
        {
            await Task.Delay(1);
            var movieResponse = new ServiceResponseDTO<string>();

            if (movieResponse.Success)
            {
                return Ok(movieResponse.Message);
            }
            else
            {
                return BadRequest(movieResponse.Message);
            }
        }

        // PUT api/<MoviesController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] MovieDTO movieToUpdate)
        {
            await Task.Delay(1);
            var movieResponse = new ServiceResponseDTO<string>();

            if (movieResponse.Success)
            {
                return Ok(movieResponse.Message);
            }
            else
            {
                return BadRequest(movieResponse.Message);
            }
        }

    }
}
