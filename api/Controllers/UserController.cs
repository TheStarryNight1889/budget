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

        [HttpGet("{id}")]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            return Ok(await _userService.Get(id));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] JObject user)
        {
            try
            {
                string email = user.GetValue("Email").ToString();

                if (await _userService.IsEmailIsAvailable(email))
                {
                   await _userService.Create(user);
                   return Ok();
                }
                else
                    return Conflict();

            } catch(Exception e)
            {
                return BadRequest();
            }

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> Put(string id, [FromBody] JObject user)
        {
            await _userService.Update(id, user);
            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.Remove(id);
            return Ok();
        }

        [Route("/account/{id}")]
        [HttpPost]
        [Authorize(Roles ="user,admin")]
        public async Task<IActionResult> PostAccount([FromRoute] string id,[FromBody] JObject account)
        {
            try
            {
                await _userService.CreateAccount(id, account);

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
    }
}
