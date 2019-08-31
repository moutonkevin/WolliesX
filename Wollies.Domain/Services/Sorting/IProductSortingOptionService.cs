using System.Collections.Generic;
using System.Threading.Tasks;
using Wollies.Contracts;

namespace Wollies.Domain.Services.Sorting
{
    public interface IProductSortingOptionService
    {
        SortingOption SortingOption { get; }

        Task<IList<Product>> SortAsync(IList<Product> products);
    }
}