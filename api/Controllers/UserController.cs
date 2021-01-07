using api.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
                return Ok(_userService.GetAll());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            return Ok(_userService.Get(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] JObject user)
        {
            _userService.Create(user);
            return Ok();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody] JObject user)
        {
            _userService.Update(id, user);
            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _userService.Remove(id);
            return Ok();
        }
    }
}
