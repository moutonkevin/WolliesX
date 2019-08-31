using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wollies.Contracts;

namespace Wollies.Domain.Services.Sorting
{
    public class AscendingNameProductSortingService : IProductSortingOptionService
    {
        public SortingOption SortingOption => SortingOption.Ascending;

        public async Task<IList<Product>> SortAsync(IList<Product> products)
        {
            return products.OrderBy(product => product.Name).ToList();
        }
    }
}
