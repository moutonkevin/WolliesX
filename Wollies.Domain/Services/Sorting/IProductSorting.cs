using System.Collections.Generic;
using System.Threading.Tasks;
using Wollies.Contracts;

namespace Wollies.Domain.Services.Sorting
{
    public interface IProductSorting
    {
        Task<IList<Product>> SortProductsByOptionAsync(SortingOption sortingOption);
    }
}