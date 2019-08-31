using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Wollies.Contracts;
using Wollies.Domain.Exceptions;
using Wollies.Domain.Services;

namespace Wollies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortController : ControllerBase
    {
        private readonly IProductSortingService _productSortingService;

        public SortController(IProductSortingService productSortingService)
        {
            _productSortingService = productSortingService;
        }

        [HttpGet]
        public async Task<ActionResult<IList<Product>>> SortAllAsync(SortingOption sortOption)
        {
            try
            {
                var sortedProducts = await _productSortingService.SortAllProductsByOptionAsync(sortOption);

                return Ok(sortedProducts);
            }
            catch (InternalServerException)
            {
                //logging
                //monitoring
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
            catch (Exception)
            {
                //logging
                //monitoring
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
