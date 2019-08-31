using System.Collections.Generic;

namespace Wollies.Contracts
{
    public class Trolley
    {
        public IList<Product> Products { get; set; }
        public IList<Special> Specials { get; set; }
        public IList<ProductQuantity> Quantities { get; set; }
    }
}
