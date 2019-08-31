using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Wollies.Contracts;
using Wollies.Domain.Clients;

namespace Wollies.Domain.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IProductsApiClient _productsClient;

        public ProductRepository(IConfiguration configuration, IProductsApiClient productsClient)
        {
            _configuration = configuration;
            _productsClient = productsClient;
        }

        public async Task<IList<Product>> GetAllAsync()
        {
            return await _productsClient.GetProductsAsync(_configuration["Token"]);
        }
    }
}
