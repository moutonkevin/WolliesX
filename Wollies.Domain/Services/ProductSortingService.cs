using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wollies.Contracts;
using Wollies.Domain.Exceptions;
using Wollies.Domain.Repositories;
using Wollies.Domain.Services.SortingStrategies;

namespace Wollies.Domain.Services
{
    public class ProductSortingService : IProductSortingService
    {
        private readonly IProductRepository _productRepository;
        private readonly IEnumerable<IProductSortingOptionService> _sortingOptionServices;

        public ProductSortingService(IProductRepository productRepository, IEnumerable<IProductSortingOptionService> sortingOptionServices)
        {
            _productRepository = productRepository;
            _sortingOptionServices = sortingOptionServices;
        }

        private IProductSortingOptionService GetProductSortingOptionService(SortingOption sortingOption)
        {
            return _sortingOptionServices.FirstOrDefault(service => service.SortingOption == sortingOption);
        }

        public async Task<IList<Product>> SortAllProductsByOptionAsync(SortingOption sortingOption)
        {
            //caching
            var products = await _productRepository.GetAllAsync();

            var sortingService = GetProductSortingOptionService(sortingOption);
            if (sortingService == null)
            {
                //logging
                //monitoring
                throw new InternalServerException($"The sorting option {sortingOption} is not available");
            }

            return await sortingService.SortAsync(products);
        }
    }
}
