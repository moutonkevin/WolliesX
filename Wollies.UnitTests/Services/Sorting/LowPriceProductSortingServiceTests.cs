using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Wollies.Contracts;
using Wollies.Domain.Services.Sorting;

namespace Wollies.UnitTests.Services.Sorting
{
    public class LowPriceProductSortingServiceTests
    {
        private IProductSortingOption _productSortingOption { get; set; }

        public LowPriceProductSortingServiceTests()
        {
            _productSortingOption = new LowPriceProductSortingService();
        }

        [Test]
        public async Task WhenSorting2Prices_TheFirstPriceIsSmallerThanTheSecondPrice()
        {
            //Arrange
            var products = new List<Product>
            {
                new Product {Name = "A", Quantity = 1, Price = 10},
                new Product {Name = "B", Quantity = 1, Price = 9}
            };

            //Act
            var sortedProducts = (await _productSortingOption.SortAsync(products)).ToArray();

            //Assert
            Assert.Less(sortedProducts[0].Price, sortedProducts[1].Price);
        }
    }
}