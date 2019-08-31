using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using Wollies.Api;

namespace Wollies.IntegrationTests
{
    public class Tests
    {
        private HttpClient _client { get; set; }

        public Tests()
        {
            var builder = new WebHostBuilder()
                //.UseContentRoot("C:\\interviews\\WolliesX\\Wollies.Api")
                .UseEnvironment("Development")
                .UseStartup<Startup>();

            var testServer = new TestServer(builder);
            _client = testServer.CreateClient();
        }

        [Test]
        public async Task Test1()
        {
            var res = await _client.GetAsync("api/user");
        }
    }
}