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
    public class WalletControllerTests : ControllerTestsBase
    {
        // Data
        public JObject PostWalletValid = JObject.Parse(File.ReadAllText(@"../../../TestData/Wallet/NewWalletValid.json"));
        public JObject PutWalletValid = JObject.Parse(File.ReadAllText(@"../../../TestData/Wallet/PutWalletValid.json"));
        public JObject PostWalletBadRequest = JObject.Parse(File.ReadAllText(@"../../../TestData/Wallet/PostWalletBadRequest.json"));
        public JObject PutWalletBadRequest = JObject.Parse(File.ReadAllText(@"../../../TestData/Wallet/PutWalletBadRequest.json"));

        [Fact]
        public async Task Get_Wallets_OK()
        {
            var response = await _userClient.GetAsync(String.Format(BASE_URL, WALLET));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Post_Wallet_OK()
        {
            var response = await _userClient.PostAsync(String.Format(BASE_URL, WALLET), new StringContent(PostWalletValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Post_Wallet_BadRequest()
        {
            var response = await _userClient.PostAsync(String.Format(BASE_URL, WALLET), new StringContent(PostWalletBadRequest.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task Delete_Wallet_OK()
        {
            var response = await _userClient.DeleteAsync(String.Format(BASE_URL_PARAM, WALLET, DELETE_WALLET_PARAM));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Delete_Wallet_BadRequest()
        {
            var response = await _userClient.DeleteAsync(String.Format(BASE_URL_PARAM, WALLET, DELETE_WALLET_PARAM_BAD_REQUEST));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task Put_Wallet_Ok()
        {
            var response = await _userClient.PutAsync(String.Format(BASE_URL, WALLET), new StringContent(PutWalletValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Put_Wallet_BadRequest()
        {
            var response = await _userClient.PutAsync(String.Format(BASE_URL, WALLET), new StringContent(PutWalletBadRequest.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
