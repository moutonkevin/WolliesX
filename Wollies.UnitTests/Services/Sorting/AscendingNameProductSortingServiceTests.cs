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
    public class AscendingNameProductSortingServiceTests
    {
        private IProductSortingOption _productSortingOption { get; set; }

        public AscendingNameProductSortingServiceTests()
        {
            _productSortingOption = new AscendingNameProductSortingService();
        }

        [Test]
        public async Task WhenSorting2NameAscending_TheFirstNameIsBeforeTheSecondName()
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
            Assert.AreEqual("A", sortedProducts[0].Name);
            Assert.AreEqual("B", sortedProducts[1].Name);
        }
    }
}
