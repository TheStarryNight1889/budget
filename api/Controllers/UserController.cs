using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private string userId;
        public UserController(IHttpContextAccessor httpContextAccessor, UserService userService)
        {
            this._userService = userService;
            this.userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(UserModel user)
        {
            if (!await _userService.IsEmailIsAvailable(user.Email))
            {
                return Conflict();
            }
            else
            {
                await _userService.Create(user);
                return Ok();
            }
        }

        [HttpPut("")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Put(UserModel user)
        {
            await _userService.Update(this.userId, user);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.Remove(id);
            return Ok();
        }

    }
}
