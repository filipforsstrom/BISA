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
            var bookResponse = await _bookService.GetBook(id);

            if (bookResponse.Success)
            {
                return Ok(bookResponse.Data);
            }
            else
            {
                return BadRequest(bookResponse.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BookCreateDTO bookToCreate)
        {

            var bookResponse = await _bookService.CreateBook(bookToCreate);

            if (bookResponse.Success)
            {
                return Ok(bookResponse.Data);
            }
            else
            {
                return BadRequest(bookResponse.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] BookUpdateDTO bookToUpdate)
        {
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
