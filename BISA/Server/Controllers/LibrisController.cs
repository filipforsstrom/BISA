﻿using BISA.Server.Services.LibrisService;
using BISA.Shared.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrisController : ControllerBase
    {
        private readonly ILibrisService _librisService;

        public LibrisController(ILibrisService librisService)
        {
            _librisService = librisService ?? throw new ArgumentNullException(nameof(librisService));
        }
        [HttpGet]
        public async Task<List<LibrisItemDTO>> GetItems()
        {
            var result = await _librisService.GetItems();
            return result;
        }
    }
}
