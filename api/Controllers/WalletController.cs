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
        private readonly WalletService _walletService;
        private string userId;
        public WalletController(IHttpContextAccessor httpContextAccessor, WalletService accountService)
        {
            this._walletService = accountService;
            this.userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
        }

        [Route("")]
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetWallets()
        {
            try
            {
                return Ok(await _walletService.GetWallets(userId));
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> PostWallet(WalletModel wallet)
        {
            try
            {
                if (wallet.Name == null)
                    throw new NullReferenceException();

                wallet.DateOffsetBalance = new List<DateOffsetBalance>();
                await _walletService.CreateWallet(userId, wallet);
                return Ok();
            } catch(Exception e)
            {
                return BadRequest();
            }

        }

        [Route("{walletId}")]
        [HttpDelete]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteWallet([FromRoute] string walletId)
        {
            try
            {
                await _walletService.DeleteWallet(userId, walletId);
                return Ok();
            } catch(Exception e)
            {
                return BadRequest();
            }

        }

        [Route("")]
        [HttpPut]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> UpdateWallet(WalletModel wallet)
        {
            try
            {
                if (wallet._id == null)
                    throw new NullReferenceException();
                await _walletService.UpdateWallet(userId, wallet);
                return Ok();
            } catch(Exception e)
            {
                return BadRequest();
            }
        }
    }
}
