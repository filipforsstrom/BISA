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
            try
            {
                var movieResponse = await _movieService.GetMovie(id);
                return Ok(movieResponse);
            }
            catch (NotFoundException exception)
            {

                return NotFound(exception.Message);
            }
            catch(Exception exeption)
            {
                return BadRequest(exeption.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Post([FromBody] MovieCreateDTO movieToCreate)
        {

            try
            {
                var movieResponse = await _movieService.CreateMovie(movieToCreate);
                return Ok(movieResponse);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }


        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Put(int id, [FromBody] MovieUpdateDTO movieToUpdate)
        {

            try
            {
                var movieResponse = await _movieService.UpdateMovie(id, movieToUpdate);
                return Ok(movieResponse);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch(ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

    }
}
