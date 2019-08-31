using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wollies.Contracts;

namespace Wollies.Domain.Services.SortingStrategies
{
    public class LowPriceProductSortingService : IProductSortingOptionService
    {
        public SortingOption SortingOption => SortingOption.Low;

        public async Task<IList<Product>> SortAsync(IList<Product> products)
        {
            return products.OrderBy(product => product.Price).ToList();
        }
    }
}
