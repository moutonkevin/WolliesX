using System.Collections.Generic;

namespace Wollies.Contracts
{
    public class Special
    {
        public IList<ProductQuantity> Quantities { get; set; }
        public decimal Total { get; set; }
    }
}
