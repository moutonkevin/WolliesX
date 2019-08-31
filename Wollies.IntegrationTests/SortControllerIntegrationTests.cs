using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NUnit.Framework;
using Wollies.Api;
using Wollies.Contracts;

namespace Wollies.IntegrationTests
{
    public class SortControllerIntegrationTests
    {
        private HttpClient _client { get; set; }

        public SortControllerIntegrationTests()
        {
            var builder = new WebHostBuilder()
                .UseContentRoot(System.AppContext.BaseDirectory)
                .UseEnvironment("Development")
                .UseConfiguration(new ConfigurationBuilder()
                    .SetBasePath(System.AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build())
                .UseStartup<Startup>();

            var testServer = new TestServer(builder);
            _client = testServer.CreateClient();
        }

        [Test]
        public async Task WhenSortIsCalled_ProductsAreReturned()
        {
            //Act
            var result = await _client.GetAsync("api/sort?sortOption=Low");

            var resultString = await result.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IList<Product>>(resultString);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.Greater(products.Count, 1);
        }
    }
}
