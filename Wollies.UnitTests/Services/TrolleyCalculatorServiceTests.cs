using System.Collections.Generic;
using NUnit.Framework;
using Wollies.Contracts;
using Wollies.Domain.Services;

namespace Wollies.UnitTests.Services
{
    public class TrolleyCalculatorServiceTests
    {
        private ITrolleyCalculator _trolleyCalculator { get; set; }

        public TrolleyCalculatorServiceTests()
        {
            _trolleyCalculator = new TrolleyCalculatorService();
        }

        [TestCase(15, 10, 9, 1, 1, 1, 1)]
        [TestCase(19, 10, 9, 1, 1, 2, 2)]
        public void WhenCalculatingTrolley_TheLowestTotalIsReturned(
            decimal expectedTotal, 
            decimal priceA, 
            decimal priceB, 
            int quantityA, 
            int quantityB, 
            int quantitySpecialA, 
            int quantitySpecialB)
        {
            //Arrange
            var trolley = new Trolley
            {
                Products = new List<Product>
                {
                    new Product {Name = "A", Price = priceA},
                    new Product {Name = "B", Price = priceB}
                },
                Specials = new List<Special>
                {
                    new Special
                    {
                        Quantities = new List<ProductQuantity>
                        {
                            new ProductQuantity {Name = "A", Quantity = quantitySpecialA},
                            new ProductQuantity {Name = "B", Quantity = quantitySpecialB}
                        },
                        Total = 15
                    }
                },
                Quantities = new List<ProductQuantity>
                {
                    new ProductQuantity {Name = "A", Quantity = quantityA},
                    new ProductQuantity {Name = "B", Quantity = quantityB}
                }
            };

            //Act
            var total = _trolleyCalculator.CalculateLowestTotal(trolley);

            //Assert
            Assert.AreEqual(expectedTotal, total);
        }
    }
}
