using api.Models;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private readonly WalletService _accountService;
        private string userId;
        public WalletController(IHttpContextAccessor httpContextAccessor, WalletService accountService)
        {
            this._accountService = accountService;
            this.userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
        }

        [Route("")]
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> PostWallet(WalletModel wallet)
        {
            await _accountService.CreateWallet(userId, wallet);
            return Ok();
        }

        [Route("{walletId}")]
        [HttpDelete]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteWallet([FromRoute] string walletId)
        {
            await _accountService.DeleteWallet(userId, walletId);
            return Ok();
        }

        [Route("")]
        [HttpPut]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> UpdateWallet(WalletModel wallet)
        {
            await _accountService.UpdateWallet(userId, wallet);
            return Ok();
        }
    }
}
