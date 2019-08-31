using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wollies.Contracts;

namespace Wollies.Domain.Services.Sorting
{
    public class LowPriceProductSortingService : IProductSortingOption
    {
        public SortingOption SortingOption => SortingOption.Low;

        public async Task<IList<Product>> SortAsync(IList<Product> products)
        {
            return products.OrderBy(product => product.Price).ToList();
        }
    }
}
