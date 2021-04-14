using api;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class ControllerTestsBase : WebApplicationFactory<Startup>
    {
        public HttpClient _unauthorizedClient { get; }
        public HttpClient _userClient { get; }
        public HttpClient _adminClient { get; }

        // URIs
        public const string BASE_URL = "api/{0}";
        public const string BASE_URL_PARAM = "api/{0}/{1}";

        public const string LOGIN = "Login";
        public const string WALLET = "Wallet";
        public const string USER = "User";

        // Parameters
        public const string NULL_MOD = "";
        public const string DELETE_WALLET_PARAM = "6075f4127017be83a95d3b3c";

        // Data
        public JObject LoginUserValid = JObject.Parse(File.ReadAllText(@"../../../TestData/LoginUserValid.json"));


        public ControllerTestsBase()
        {
            _unauthorizedClient = this.CreateServer().CreateClient();
            _userClient = this.CreateAuthenticatedClient();
        }
        public TestServer CreateServer()
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder();
            webHostBuilder.UseStartup<Startup>();
            return new TestServer(webHostBuilder);
        }
        public HttpClient CreateAuthenticatedClient()
        {
            var response = _unauthorizedClient.PostAsync(String.Format(BASE_URL, LOGIN), new StringContent(LoginUserValid.ToString(), Encoding.UTF8, "application/json"));
            JObject content = JObject.Parse(response.Result.Content.ReadAsStringAsync().Result);
            string token = content.GetValue("token").ToString();

            HttpClient client = this.CreateServer().CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            return client;
        }
    }
}
