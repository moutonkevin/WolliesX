using System.Collections.Generic;
using System.Linq;
using Wollies.Contracts;

namespace Wollies.Domain.Services
{
    public class TrolleyCalculatorService : ITrolleyCalculator
    {
        private bool IsSpecialActivated(IList<ProductQuantity> specialQuantities, IList<ProductQuantity> productQuantities)
        {
            foreach (var specialQuantity in specialQuantities)
            {
                if (productQuantities.Any(p => p.Quantity - specialQuantity.Quantity < 0))
                    return false;
            }

            return true;
        }

        public int ProductsLeftToProcess(IList<ProductQuantity> productQuantities)
        {
            return productQuantities.Sum(p => p.Quantity);
        }

        private void DecreaseQuantityForProductName(IList<ProductQuantity> productQuantities, string name, int value)
        {
            var productQuantity = productQuantities.FirstOrDefault(p => p.Name.Equals(name));
            if (productQuantity != null)
                productQuantity.Quantity -= value;
        }

        private decimal GetPriceFromProductName(IList<Product> products, string name)
        {
            var product = products.FirstOrDefault(p => p.Name.Equals(name));
            return product?.Price ?? 0;
        }

        public decimal CalculateLowestTotal(Trolley trolley)
        {
            decimal total = 0;

            while (ProductsLeftToProcess(trolley.Quantities) > 0)
            {
                foreach (var special in trolley.Specials)
                {
                    if (IsSpecialActivated(special.Quantities, trolley.Quantities))
                    {
                        foreach (var specialQuantity in special.Quantities)
                        {
                            DecreaseQuantityForProductName(trolley.Quantities, specialQuantity.Name, specialQuantity.Quantity);
                        }
                        total += special.Total;
                    }
                }

                foreach (var productQuantity in trolley.Quantities)
                {
                    if (productQuantity.Quantity > 0)
                    {
                        total += GetPriceFromProductName(trolley.Products, productQuantity.Name);
                        DecreaseQuantityForProductName(trolley.Quantities, productQuantity.Name, 1);
                    }
                }
            }
            return total;
        }
    }
}
