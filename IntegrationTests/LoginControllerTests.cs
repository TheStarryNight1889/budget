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

namespace IntegrationTests
{
    public class LoginControllerTests : ControllerTestsBase
    {
        public LoginControllerTests(WebApplicationFactory<api.Startup> fixture) : base(fixture) { }

        [Fact]
        public async Task Login_Valid_Credentials_OK()
        {
            var response = await _client.PostAsync("/api/Login", new StringContent(LoginUserValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
