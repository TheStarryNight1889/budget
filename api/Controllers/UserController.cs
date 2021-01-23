using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Get()
        {
                return Ok(await _userService.GetAll());
        }

        [HttpGet("{email}")]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> Get(string email)
        {
            return Ok(await _userService.Get(email));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] JObject user)
        {
            if(user.GetValue("email").ToString() == "")
            {
                return BadRequest();
            }

            JObject u = await _userService.Get(user.GetValue("email").ToString());

            if(u.GetValue("email") == user.GetValue("email"))
            {
                return Conflict();
            }

            try
            {
                await _userService.Create(user);
            }
            catch(Exception e) { Console.WriteLine("POST USER EXCEPTION:\n " + e.ToString()); return BadRequest(); }
            return Ok();
        }

        [HttpPut("{email}")]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> Put(string email, [FromBody] JObject user)
        {
            await _userService.Update(email, user);
            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{email}")]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> Delete(string email)
        {
            await _userService.Remove(email);
            return Ok();
        }
    }
}
