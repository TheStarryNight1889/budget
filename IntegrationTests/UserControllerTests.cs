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
    public class UserControllerTests: ControllerTestsBase 
    {
        public UserControllerTests(WebApplicationFactory<api.Startup> fixture) : base(fixture) { }
        [Fact]
        public async Task Post_User_OK()
        {
            var response = await _client.PostAsync("/api/User", new StringContent(NewUserValid.ToString(), Encoding.UTF8, "application/json"));
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
