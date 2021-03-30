﻿using api.Helpers;
using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserService _userService;
        public LoginController(UserService userService)
        {
            this._userService = userService;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(CredentialsModel credentials)
        {   
            try
            {
                var user = await _userService.Authenticate(credentials);

                var token = TokenService.CreateToken(user);
                user.Password = "";

                JObject json = new JObject
                {
                    ["user"] = JToken.FromObject(user),
                    ["token"] = token
                };
                return Ok(json);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
    }
}
