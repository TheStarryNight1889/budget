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
    public class RecurringTransactionController : ControllerBase
    {
        private readonly RecurringTransactionService _recurringTransactionService;
        private string userId;
        public RecurringTransactionController(IHttpContextAccessor httpContextAccessor, RecurringTransactionService recurringtransactionService)
        {
            this._recurringTransactionService = recurringtransactionService;
            this.userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
        }

        [Route("{walletId}")]
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetRecurringTransactionByWallet([FromRoute] string walletId)
        {
            try
            {
                if (walletId == null)
                    throw new NullReferenceException();
                return Ok(await _recurringTransactionService.GetRecurringTransactionsForWallet(userId, walletId));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetTransactionByUser()
        {
            try
            {
                return Ok(await _recurringTransactionService.GetRecurringTransactionsForUser(userId));
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> PostRecurringTransaction(RecurringTransactionModel recurringTransaction)
        {
            try
            {
                if (recurringTransaction.Name == null)
                    throw new NullReferenceException();

                await _recurringTransactionService.CreateRecurringTransaction(userId, recurringTransaction);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Route("{recurringTransactionId}")]
        [HttpDelete]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteRecurringTransaction([FromRoute] string recurringTransactionId)
        {
            try
            {
                if (recurringTransactionId == null)
                    throw new NullReferenceException();
                await _recurringTransactionService.DeleteRecurringTransaction(recurringTransactionId);
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
        public async Task<IActionResult> UpdateTransaction(RecurringTransactionModel recurringTransaction)
        {
            try
            {
                if (recurringTransaction._id == null)
                    throw new NullReferenceException();
                await _recurringTransactionService.UpdateRecurringTransaction(recurringTransaction);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
