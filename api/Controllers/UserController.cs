using api.Services;
using Microsoft.AspNetCore.Authorization;
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
        private readonly System.Security.Claims.ClaimsPrincipal _currentUser;
        public UserController(UserService userService)
        {
            this._userService = userService;
            this._currentUser = this.User;
        }
        // GET: api/<UserController>
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get()
        {
                return Ok(_userService.GetAll());
        }

        // GET api/<UserController>/5
        [HttpGet("{email}")]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> Get(string email)
        {
            return Ok(_userService.Get(email));
        }

        // POST api/<UserController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] JObject user)
        {
            _userService.Create(user);
            return Ok();
        }

        // PUT api/<UserController>/5
        [HttpPut("{email}")]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> Put(string email, [FromBody] JObject user)
        {
            _userService.Update(email, user);
            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{email}")]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> Delete(string email)
        {
            _userService.Remove(email);
            return Ok();
        }
    }
}
