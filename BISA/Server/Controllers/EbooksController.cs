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

        [HttpPost]
        [Authorize(Roles = "Admin, Staff")]
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

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Staff")]
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
