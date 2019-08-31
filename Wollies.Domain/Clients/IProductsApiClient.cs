using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Wollies.Contracts;

namespace Wollies.Domain.Clients
{
    public interface IProductsApiClient
    {
        [Get("/api/resource/products?token={token}")]
        Task<IList<Product>> GetProductsAsync([AliasAs("token")] string token);
    }
}
