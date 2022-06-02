using BISA.Server.Services.BookService;
using BISA.Shared.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var bookResponse = await _bookService.GetBook(id);
                return Ok(bookResponse);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
        public async Task<IActionResult> Post([FromBody] BookCreateDTO bookToCreate)
        {

            try
            {
                var bookResponse = await _bookService.CreateBook(bookToCreate);
                return Ok(bookResponse);
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
        public async Task<IActionResult> Put(int id, [FromBody] BookUpdateDTO bookToUpdate)
        {
            bookToUpdate.Id = id;
            var bookResponse = await _bookService.UpdateBook(bookToUpdate);

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
