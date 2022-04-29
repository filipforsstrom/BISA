﻿using Microsoft.AspNetCore.Mvc;

namespace BISA.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        // GET: api/<StatisticsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<StatisticsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<StatisticsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<StatisticsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StatisticsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
