using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Wollies.Contracts;
using Wollies.Domain.Exceptions;
using Wollies.Domain.Services.Sorting;

namespace Wollies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        private readonly IProductSorting _productSortingService;
        private readonly ILogger<SortController> _logger;

        public SortController(IProductSorting productSortingService, ILogger<SortController> logger)
        {
            _productSortingService = productSortingService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Product>>> SortProductsAsync(SortingOption sortOption)
        {
            try
            {
                var sortedProducts = await _productSortingService.SortProductsByOptionAsync(sortOption);

                return Ok(sortedProducts);
            }
            catch (InternalServerException exception)
            {
                //TODO Monitoring
                _logger.LogError("Could not sort the products", exception);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception exception)
            {
                //TODO Monitoring
                _logger.LogError("An unexpected error happened", exception);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
