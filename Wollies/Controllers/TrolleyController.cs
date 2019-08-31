using Microsoft.AspNetCore.Mvc;
using Wollies.Contracts;
using Wollies.Domain.Services;

namespace Wollies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyController : ControllerBase
    {
        private readonly ITrolleyCalculatorService _calculatorService;

        public TrolleyController(ITrolleyCalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        [HttpPost]
        [Route("trolleyTotal")]
        public decimal GetTotal(Trolley trolley)
        {
            var total = _calculatorService.CalculateLowestTotal(trolley);

            return total;
        }
    }
}
