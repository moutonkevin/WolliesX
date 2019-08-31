using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Wollies.Contracts;
using Wollies.Domain.Clients;

namespace Wollies.Domain.Services.Sorting
{
    public class RecommendedProductSortingService : IProductSortingOptionService
    {
        public SortingOption SortingOption => SortingOption.Recommended;

        private readonly IConfiguration _configuration;
        private readonly IShopperHistoryApiClient _shopperHistoryApiClient;

        public RecommendedProductSortingService(IConfiguration configuration, IShopperHistoryApiClient shopperHistoryApiClient)
        {
            _configuration = configuration;
            _shopperHistoryApiClient = shopperHistoryApiClient;
        }

        public async Task<IList<Product>> SortAsync(IList<Product> products)
        {
            var shopperHistory = await _shopperHistoryApiClient.GetShopperHistoryAsync(_configuration["Token"]);

            var productsOrderedByPopularity = shopperHistory
                .SelectMany(c => c.Products)
                .Concat(products)
                .GroupBy(g => g.Name)
                .Select(s => new
                {
                    ProductName = s.Key,
                    NumberSold = s.Sum(c => c.Quantity),
                    Product = s.FirstOrDefault()
                })
                .OrderByDescending(p => p.NumberSold)
                .Select(s => s.Product)
                .ToList();

            return productsOrderedByPopularity;
        }
    }
}
