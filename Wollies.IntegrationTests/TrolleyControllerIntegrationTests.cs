using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
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
    public class TrolleyControllerIntegrationTests
    {
        private HttpClient _client { get; set; }

        public TrolleyControllerIntegrationTests()
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
        public async Task WhenTrolleyTotalIsCalledTotalIsReturned()
        {
            //arrange
            var trolley = new Trolley
            {
                Products = new List<Product>
                {
                    new Product {Name = "A", Price = 10},
                    new Product {Name = "B", Price = 5}
                },
                Specials = new List<Special>
                {
                    new Special
                    {
                        Quantities = new List<ProductQuantity>
                        {
                            new ProductQuantity {Name = "A", Quantity = 1},
                            new ProductQuantity {Name = "B", Quantity = 1}
                        },
                        Total = 15
                    }
                },
                Quantities = new List<ProductQuantity>
                {
                    new ProductQuantity {Name = "A", Quantity = 1},
                    new ProductQuantity {Name = "B", Quantity = 1}
                }
            };

            //Act
            var result = await _client.PostAsync("api/trolley/trolleyTotal", new StringContent(JsonConvert.SerializeObject(trolley), Encoding.UTF8, "application/json"));

            var resultString = await result.Content.ReadAsStringAsync();
            var total = JsonConvert.DeserializeObject<decimal>(resultString);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(15, total);
        }
    }
}
