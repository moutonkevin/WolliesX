using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Wollies.Contracts;

namespace Wollies.Domain.Repositories
{
    public interface IShopperHistoryApiClient
    {
        [Get("/api/resource/shopperHistory?token={token}")]
        Task<IList<Customer>> GetShopperHistoryAsync([AliasAs("token")] string token);
    }
}
