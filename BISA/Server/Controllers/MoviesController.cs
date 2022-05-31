using BISA.Server.Services.MovieService;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
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

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Put(int id, [FromBody] MovieUpdateDTO movieToUpdate)
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
