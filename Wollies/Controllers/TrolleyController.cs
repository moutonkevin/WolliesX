using Microsoft.AspNetCore.Mvc;
using Wollies.Contracts;
using Wollies.Domain.Services;

namespace Wollies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyController : ControllerBase
    {
        private readonly ITrolleyCalculator _calculatorService;

        public TrolleyController(ITrolleyCalculator calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpPost]
        [Route("trolleyTotal")]
        public decimal GetTotal(Trolley trolley)
        {
            //TODO Logging / Validation / Monitoring
            return _calculatorService.CalculateLowestTotal(trolley);
        }
    }
}
