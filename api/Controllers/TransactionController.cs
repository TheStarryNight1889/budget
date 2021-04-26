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
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;
        private string userId;
        public TransactionController(IHttpContextAccessor httpContextAccessor, TransactionService transactionService)
        {
            this._transactionService = transactionService;
            this.userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value.ToString();
        }

        [Route("{walletId}")]
        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetTransactionByWallet([FromRoute] string walletId)
        {
            try
            {
                if (walletId == null)
                    throw new NullReferenceException();
                return Ok(await _transactionService.GetTransactionsForWallet(userId, walletId));
            } catch(Exception e)
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
                return Ok(await _transactionService.GetTransactionsForUser(userId));
            } catch(Exception e)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpPost]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> PostTransaction(TransactionModel transaction)
        {
            try
            {
                if (transaction.WalletId == null)
                    throw new NullReferenceException();

                await _transactionService.CreateTransaction(userId, transaction);
                return Ok();
            } catch (Exception e)
            {
                return BadRequest();
            }
        }

        [Route("{transactionId}")]
        [HttpDelete]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> DeleteTransaction([FromRoute] string transactionId)
        {
            try
            {
                if (transactionId == null)
                    throw new NullReferenceException();
                await _transactionService.DeleteTransaction(userId, transactionId);
                return Ok();
            } catch(Exception e)
            {
                return NotFound();
            }
        }

        [Route("")]
        [HttpPut]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> UpdateTransaction(TransactionModel transaction)
        {
            try
            {
                if (transaction._id == null)
                    throw new NullReferenceException();
                await _transactionService.UpdateTransaction(userId, transaction);
                return Ok();
            } catch(Exception e)
            {
                return BadRequest();
            }
        }
    }
}
