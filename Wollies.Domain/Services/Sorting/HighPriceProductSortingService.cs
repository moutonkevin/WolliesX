using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wollies.Contracts;

namespace Wollies.Domain.Services.Sorting
{
    public class HighPriceProductSortingService : IProductSortingOptionService
    {
        public SortingOption SortingOption => SortingOption.High;

        public async Task<IList<Product>> SortAsync(IList<Product> products)
        {
            return products.OrderByDescending(product => product.Price).ToList();
        }
    }
}
