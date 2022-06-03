using BISA.Server.Services.EbookService;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EbooksController : ControllerBase
    {
        private readonly IEbookService _ebookService;

        public EbooksController(IEbookService ebookService)
        {
            _ebookService = ebookService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var ebook = await _ebookService.GetEbook(id);
                return Ok(ebook);
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
        public async Task<IActionResult> Post([FromBody] EbookCreateDTO ebookToCreate)
        {
            try
            {
                var ebookResponse = await _ebookService.CreateEbook(ebookToCreate);
                return Ok(ebookResponse);
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
        public async Task<IActionResult> Put(int id, [FromBody] EbookUpdateDTO ebookToUpdate)
        {
            try
            {
                ebookToUpdate.Id = id;
                var ebookResponse = await _ebookService.UpdateEbook(ebookToUpdate);
                return Ok(ebookResponse);
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (NotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception exception)
            {
                return StatusCode(500, exception.Message);
            }
            
        }
    }
}
