using System.Collections.Generic;
using System.Threading.Tasks;
using Wollies.Contracts;

namespace Wollies.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IList<Product>> GetAllAsync();
    }
}