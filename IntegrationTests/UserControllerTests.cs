using Microsoft.AspNetCore.Mvc.Testing;
using System;
using Xunit;
using api;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using FluentAssertions;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json.Linq;
using System.IO;

namespace IntegrationTests
{
    public class UserControllerTests: ControllerTestsBase 
    {
        // Data 
        public JObject NewUserValid = JObject.Parse(File.ReadAllText(@"../../../TestData/NewUserValid.json"));
        public JObject NewUserBadRequest = JObject.Parse(File.ReadAllText(@"../../../TestData/NewUserBadRequest.json"));
        public JObject PutUserValid = JObject.Parse(File.ReadAllText(@"../../../TestData/PutUserValid.json"));

        [Fact]
        public async Task Post_User_OK()
        {
            var response = await _userClient.PostAsync(String.Format(BASE_URL, USER), new StringContent(NewUserValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        [Fact]
        public async Task Post_User_BadRequest()
        {
            var response = await _userClient.PostAsync(String.Format(BASE_URL, USER), new StringContent(NewUserBadRequest.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task Post_User_Conflict()
        {
            var response = await _userClient.PostAsync(String.Format(BASE_URL, USER), new StringContent(NewUserBadRequest.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
        [Fact]
        public async Task Put_User_OK()
        {
            var response = await _userClient.PutAsync(String.Format(BASE_URL, USER), new StringContent(PutUserValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
