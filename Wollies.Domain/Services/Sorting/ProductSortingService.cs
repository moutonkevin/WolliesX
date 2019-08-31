using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Wollies.Contracts;
using Wollies.Domain.Exceptions;
using Wollies.Domain.Repositories;

namespace Wollies.Domain.Services.Sorting
{
    public class ProductSortingService : IProductSorting
    {
        private readonly IProductsApiClient _apiClient;
        private readonly IConfiguration _configuration;
        private readonly IEnumerable<IProductSortingOption> _sortingOptionServices;

        public ProductSortingService(IProductsApiClient apiClient, IConfiguration configuration, IEnumerable<IProductSortingOption> sortingOptionServices)
        {
            _apiClient = apiClient;
            _configuration = configuration;
            _sortingOptionServices = sortingOptionServices;
        }

        public async Task<IList<Product>> SortProductsByOptionAsync(SortingOption sortingOption)
        {
            //TODO caching
            var products = await _apiClient.GetProductsAsync(_configuration["Token"]);

            var sortingService = _sortingOptionServices.FirstOrDefault(service => service.SortingOption == sortingOption);
            if (sortingService == null)
            {
                //TODO Logging / Monitoring
                throw new InternalServerException($"The sorting option {sortingOption} is not available");
            }

            return await sortingService.SortAsync(products);
        }
    }
}
