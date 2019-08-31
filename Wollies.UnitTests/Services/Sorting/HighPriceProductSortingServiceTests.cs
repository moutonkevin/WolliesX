using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Wollies.Contracts;
using Wollies.Domain.Services.Sorting;

namespace Wollies.UnitTests.Services.Sorting
{
    public class HighPriceProductSortingServiceTests
    {
        private IProductSortingOption _productSortingOption { get; set; }

        public HighPriceProductSortingServiceTests()
        {
            _productSortingOption = new HighPriceProductSortingService();
        }

        [Test]
        public async Task WhenSorting2Prices_TheFirstPriceIsHigherThanTheSecondPrice()
        {
            //Arrange
            var products = new List<Product>
            {
                new Product {Name = "A", Quantity = 1, Price = 9},
                new Product {Name = "B", Quantity = 1, Price = 10}
            };

            //Act
            var sortedProducts = (await _productSortingOption.SortAsync(products)).ToArray();

            //Assert
            Assert.Greater(sortedProducts[0].Price, sortedProducts[1].Price);
        }
    }
}
