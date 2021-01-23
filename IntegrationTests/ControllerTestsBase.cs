using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests
{
    public class ControllerTestsBase : IClassFixture<WebApplicationFactory<api.Startup>>
    {
        public HttpClient _client { get; }

        //TestData
        public JObject NewUserValid = JObject.Parse(File.ReadAllText(@"../../../TestData/NewUserValid.json"));
        public JObject NewUserBadRequest = JObject.Parse(File.ReadAllText(@"../../../TestData/NewUserBadRequest.json"));
        public ControllerTestsBase(WebApplicationFactory<api.Startup> fixture)
        {
            _client = fixture.CreateClient();
        }
    }
}
