using BISA.Server.Services.EbookService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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


        // GET api/<EbooksController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
           
            var ebookResponse = await _ebookService.GetEbook(id);

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
        public async Task<IActionResult> Post([FromBody] EbookCreateDTO ebookToAdd)
        {

            var ebookResponse = await _ebookService.CreateEbook(ebookToAdd);

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
        public async Task<IActionResult> Put(int id, [FromBody] EbookUpdateDTO ebookToUpdate)
        {
            ebookToUpdate.Id = id;
            var ebookResponse = await _ebookService.UpdateEbook(ebookToUpdate);

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
