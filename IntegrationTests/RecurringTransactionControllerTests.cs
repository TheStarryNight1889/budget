using api;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class RecurringTransactionsControllerTests : ControllerTestsBase
    {
        // Data
        public JObject PostRecurringTransactionValid = JObject.Parse(File.ReadAllText(@"../../../TestData/RecurringTransactions/PostRecurringTransactionValid.json"));
        public JObject PostRecurringTransactionBadRequest = JObject.Parse(File.ReadAllText(@"../../../TestData/RecurringTransactions/PostRecurringTransactionBadRequest.json"));
        public JObject PutRecurringTransactionValid = JObject.Parse(File.ReadAllText(@"../../../TestData/RecurringTransactions/PutRecurringTransactionValid.json"));
        public JObject PutRecurringTransactionBadRequest = JObject.Parse(File.ReadAllText(@"../../../TestData/RecurringTransactions/PutRecurringTransactionBadRequest.json"));

        [Fact]
        public async Task Post_RecurringTransaction_OK()
        {
            var response = await _userClient.PostAsync(String.Format(BASE_URL, RECURRING_TRANSACTION), new StringContent(PostRecurringTransactionValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Post_RecurringTransaction_BadRequest()
        {
            var response = await _userClient.PostAsync(String.Format(BASE_URL, RECURRING_TRANSACTION), new StringContent(PostRecurringTransactionBadRequest.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_RecurringTransaction_OK()
        {
            var response = await _userClient.DeleteAsync(String.Format(BASE_URL_PARAM, RECURRING_TRANSACTION, DELETE_RECURRING_TRANSACTION_PARAM));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Delete_RecurringTransaction_NotFound()
        {
            var response = await _userClient.DeleteAsync(String.Format(BASE_URL_PARAM, RECURRING_TRANSACTION, DELETE_RECURRING_TRANSACTION_PARAM_NOT_FOUND));
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Put_RecurringTransaction_Ok()
        {
            var response = await _userClient.PutAsync(String.Format(BASE_URL, RECURRING_TRANSACTION), new StringContent(PutRecurringTransactionValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Put_RecurringTransaction_BadRequest()
        {
            var response = await _userClient.PutAsync(String.Format(BASE_URL, RECURRING_TRANSACTION), new StringContent(PutRecurringTransactionBadRequest.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_RecurringTransactions_Wallet_OK()
        {
            var response = await _userClient.GetAsync(String.Format(BASE_URL_PARAM, RECURRING_TRANSACTION, RECURRING_TRANSACTION_WALLET_PARAM));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Get_RecurringTransactions_Wallet_NotFound()
        {
            var response = await _userClient.GetAsync(String.Format(BASE_URL_PARAM, RECURRING_TRANSACTION, RECURRING_TRANSACTION_WALLET_PARAM_NOTFOUND));
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_RecurringTransactions_User_OK()
        {
            var response = await _userClient.GetAsync(String.Format(BASE_URL, RECURRING_TRANSACTION));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}
