using api.Models;
using api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class IncomeControllerTests : ControllerTestsBase
    {
        // Data
        public JObject PostIncomeValid = JObject.Parse(File.ReadAllText(@"../../../TestData/Income/PostIncomeValid.json"));
        public JObject PostIncomeBadRequest = JObject.Parse(File.ReadAllText(@"../../../TestData/Income/PostIncomeBadRequest.json"));
        public JObject PutIncomeValid = JObject.Parse(File.ReadAllText(@"../../../TestData/Income/PutIncomeValid.json"));
        public JObject PutIncomeBadRequest = JObject.Parse(File.ReadAllText(@"../../../TestData/Income/PutIncomeBadRequest.json"));

        [Fact]
        public async Task Post_Income_OK()
        {
            var response = await _userClient.PostAsync(String.Format(BASE_URL, INCOME), new StringContent(PostIncomeValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Post_Income_BadRequest()
        {
            var response = await _userClient.PostAsync(String.Format(BASE_URL, INCOME), new StringContent(PostIncomeBadRequest.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Delete_Income_OK()
        {
            var response = await _userClient.DeleteAsync(String.Format(BASE_URL_PARAM, INCOME, DELETE_INCOME_PARAM));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Delete_Income_NotFound()
        {
            var response = await _userClient.DeleteAsync(String.Format(BASE_URL_PARAM, INCOME, DELETE_INCOME_PARAM_NOT_FOUND));
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Put_Income_Ok()
        {
            var response = await _userClient.PutAsync(String.Format(BASE_URL, INCOME), new StringContent(PutIncomeValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Put_Income_BadRequest()
        {
            var response = await _userClient.PutAsync(String.Format(BASE_URL, INCOME), new StringContent(PutIncomeBadRequest.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Get_Incomes_Wallet_OK()
        {
            var response = await _userClient.GetAsync(String.Format(BASE_URL_PARAM, INCOME, INCOME_WALLET_PARAM));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Get_Incomes_Wallet_NotFound()
        {
            var response = await _userClient.GetAsync(String.Format(BASE_URL_PARAM, INCOME, INCOME_WALLET_PARAM_NOTFOUND));
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Get_Incomes_User_OK()
        {
            var response = await _userClient.GetAsync(String.Format(BASE_URL, INCOME));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
