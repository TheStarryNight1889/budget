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
        public JObject PostWalletValid = JObject.Parse(File.ReadAllText(@"../../../TestData/NewWalletValid.json"));
        public JObject PutWalletValid = JObject.Parse(File.ReadAllText(@"../../../TestData/PutWalletValid.json"));

        [Fact]
        public async Task Post_Wallet_OK()
        {
            var response = await _userClient.PostAsync(String.Format(BASE_URL, WALLET), new StringContent(PostWalletValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Delete_Wallet_OK()
        {
            var response = await _userClient.DeleteAsync(String.Format(BASE_URL_PARAM, WALLET, DELETE_WALLET_PARAM));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Put_Wallet_Ok()
        {
            var response = await _userClient.PutAsync(String.Format(BASE_URL, WALLET), new StringContent(PutWalletValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
