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
        public const string TRANSACTION = "Transaction";
        public const string RECURRING_TRANSACTION = "RecurringTransaction";
        public const string INCOME = "Income";

        // Parameters
        public const string NULL_MOD = "";
        public const string DELETE_WALLET_PARAM = "6075f4127017be83a95d3b3c";
        public const string DELETE_WALLET_PARAM_BAD_REQUEST = "605f4127017be83a95d3b3c";

        public const string DELETE_TRANSACTION_PARAM = "607d71daf95c47fcb04c4e19";
        public const string DELETE_TRANSACTION_PARAM_NOT_FOUND = "607d71daf95c47fcg04c4e19";
        public const string TRANSACTION_WALLET_PARAM = "6085f4127017be83a95d3b3c";
        public const string TRANSACTION_WALLET_PARAM_NOTFOUND = "6085j4127017be83a95d3b3c";

        public const string DELETE_RECURRING_TRANSACTION_PARAM = "607f0dbb93542f49047f6ec1";
        public const string DELETE_RECURRING_TRANSACTION_PARAM_NOT_FOUND = "607h71daf95c47fcg04c4e19";
        public const string RECURRING_TRANSACTION_WALLET_PARAM = "6085f4127017be83a95d3b3c";
        public const string RECURRING_TRANSACTION_WALLET_PARAM_NOTFOUND = "6085j4127017be83a95d3b3c";

        public const string DELETE_INCOME_PARAM = "5081931d1229115f29c03059";
        public const string DELETE_INCOME_PARAM_NOT_FOUND = "081931d1229115f29c03059";
        public const string INCOME_WALLET_PARAM = "6085f4127017be83a95d3b3c";
        public const string INCOME_WALLET_PARAM_NOTFOUND = "6085j4127017be83a95d3b3c";

        // Data
        public JObject LoginUserValid = JObject.Parse(File.ReadAllText(@"../../../TestData/Login/LoginUserValid.json"));


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
