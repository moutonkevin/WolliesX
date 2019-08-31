using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Wollies.Contracts;
using Wollies.Domain.Repositories;
using Wollies.Domain.Services.Sorting;

namespace Wollies.UnitTests.Services.Sorting
{
    public class RecommendedProductSortingServiceTests
    {
        private IProductSortingOption _productSortingOption { get; set; }

        public RecommendedProductSortingServiceTests()
        {
            var configurationMock = new Mock<IConfiguration>();
            var shopperApiClientMock = new Mock<IShopperHistoryApiClient>();

            configurationMock
                .SetupGet(m => m[It.IsAny<string>()])
                .Returns("token");

            shopperApiClientMock
                .Setup(m => m.GetShopperHistoryAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<Customer>
                {
                    new Customer
                    {
                        Products = new List<Product>
                        {
                            new Product {Name = "A", Quantity = 1, Price = 10},
                            new Product {Name = "B", Quantity = 2, Price = 10},
                            new Product {Name = "C", Quantity = 3, Price = 10},
                        }
                    },
                    new Customer
                    {
                        Products = new List<Product>
                        {
                            new Product {Name = "A", Quantity = 2, Price = 10},
                            new Product {Name = "C", Quantity = 1, Price = 10},
                        }
                    }
                });

            _productSortingOption = new RecommendedProductSortingService(configurationMock.Object, shopperApiClientMock.Object);
        }

        [Test]
        public async Task WhenSortingWithRecommended_TheMostRecommendedAppearFirst()
        {
            //Arrange
            var products = new List<Product>
            {
                new Product {Name = "D", Quantity = 0, Price = 10}
            };

            //Act
            var sortedProducts = (await _productSortingOption.SortAsync(products)).ToArray();

            //Assert
            Assert.AreEqual("C", sortedProducts[0].Name);
            Assert.AreEqual("A", sortedProducts[1].Name);
            Assert.AreEqual("B", sortedProducts[2].Name);
            Assert.AreEqual("D", sortedProducts[3].Name);
        }
    }
}
