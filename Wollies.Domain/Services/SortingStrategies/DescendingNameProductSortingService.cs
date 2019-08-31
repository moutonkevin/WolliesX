using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wollies.Contracts;

namespace Wollies.Domain.Services.SortingStrategies
{
    public class DescendingNameProductSortingService : IProductSortingOptionService
    {
        public SortingOption SortingOption => SortingOption.Descending;

        public async Task<IList<Product>> SortAsync(IList<Product> products)
        {
            return products.OrderByDescending(product => product.Name).ToList();
        }
    }
}
