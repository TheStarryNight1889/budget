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
    public class TransactionsControllerTests : ControllerTestsBase
    {
        // Data
        public JObject PostTransactionValid = JObject.Parse(File.ReadAllText(@"../../../TestData/Transactions/PostTransactionValid.json"));
        public JObject PostTransactionBadRequest = JObject.Parse(File.ReadAllText(@"../../../TestData/Transactions/PostTransactionBadRequest.json"));
        public JObject PutTransactionValid = JObject.Parse(File.ReadAllText(@"../../../TestData/Transactions/PutTransactionValid.json"));
        public JObject PutTransactionBadRequest = JObject.Parse(File.ReadAllText(@"../../../TestData/Transactions/PutTransactionBadRequest.json"));

        [Fact]
        public async Task Post_Transaction_OK()
        {
            var response = await _userClient.PostAsync(String.Format(BASE_URL, TRANSACTION), new StringContent(PostTransactionValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Post_Transaction_BadRequest()
        {
            var response = await _userClient.PostAsync(String.Format(BASE_URL, TRANSACTION), new StringContent(PostTransactionBadRequest.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_Transaction_OK()
        {
            var response = await _userClient.DeleteAsync(String.Format(BASE_URL_PARAM, TRANSACTION, DELETE_TRANSACTION_PARAM));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Delete_Transaction_NotFound()
        {
            var response = await _userClient.DeleteAsync(String.Format(BASE_URL_PARAM, TRANSACTION, DELETE_TRANSACTION_PARAM_NOT_FOUND));
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Put_Transaction_Ok()
        {
            var response = await _userClient.PutAsync(String.Format(BASE_URL, TRANSACTION), new StringContent(PutTransactionValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Put_Transaction_BadRequest()
        {
            var response = await _userClient.PutAsync(String.Format(BASE_URL, TRANSACTION), new StringContent(PutTransactionBadRequest.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_Transactions_Wallet_OK()
        {
            var response = await _userClient.GetAsync(String.Format(BASE_URL_PARAM, TRANSACTION, TRANSACTION_WALLET_PARAM));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Get_Transactions_Wallet_NotFound()
        {
            var response = await _userClient.GetAsync(String.Format(BASE_URL_PARAM, TRANSACTION, TRANSACTION_WALLET_PARAM_NOTFOUND));
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Transactions_User_OK()
        {
            var response = await _userClient.GetAsync(String.Format(BASE_URL, TRANSACTION));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

    }
}
