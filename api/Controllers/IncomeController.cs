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
    public class IncomeController : ControllerBase
    {
        private readonly IncomeService _incomeService;
        private string userId;
        public IncomeController(IHttpContextAccessor httpContextAccessor, IncomeService incomeService)
        {
            this._incomeService = incomeService;
            this.userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
        }

        [Route("{walletId}")]
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetIncomeByWallet([FromRoute] string walletId)
        {
            try
            {
                if (walletId == null)
                    throw new NullReferenceException();
                return Ok(await _incomeService.GetIncomeForWallet(userId, walletId));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetIncomeByUser()
        {
            try
            {
                return Ok(await _incomeService.GetIncomeForUser(userId));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> PostIncome(IncomeModel income)
        {
            try
            {
                if (income.WalletId == null)
                    throw new NullReferenceException();

                await _incomeService.CreateIncome(userId, income);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Route("{incomeId}")]
        [HttpDelete]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteIncome([FromRoute] string incomeId)
        {
            try
            {
                if (incomeId == null)
                    throw new NullReferenceException();
                await _incomeService.DeleteIncome(userId, incomeId);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpPut]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> UpdateIncome(IncomeModel income)
        {
            try
            {
                if (income._id == null)
                    throw new NullReferenceException();
                await _incomeService.UpdateIncome(userId, income);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
