using System.Collections.Generic;
using System.Threading.Tasks;
using Wollies.Contracts;

namespace Wollies.Domain.Services
{
    public interface IProductSortingService
    {
        Task<IList<Product>> SortAllProductsByOptionAsync(SortingOption sortingOption);
    }
}