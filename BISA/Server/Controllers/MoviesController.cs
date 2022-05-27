using BISA.Server.Services.MovieService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BISA.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        // GET api/<MoviesController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            
            var movieResponse = await _movieService.GetMovie(id);

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
        public async Task<IActionResult> Post([FromBody] MovieCreateDTO movieToCreate)
        {
            var movieResponse = await _movieService.CreateMovie(movieToCreate);

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
        public async Task<IActionResult> Put(int id,[FromBody] MovieUpdateDTO movieToUpdate)
        {
            var movieResponse = await _movieService.UpdateMovie(id, movieToUpdate);

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
