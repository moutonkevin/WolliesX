using System.Collections.Generic;

namespace Wollies.Contracts
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public IList<Product> Products { get; set; }
    }
}
