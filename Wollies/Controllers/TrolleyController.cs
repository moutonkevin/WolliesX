using Microsoft.AspNetCore.Mvc;
using Wollies.Contracts;

namespace Wollies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyController : ControllerBase
    {
        public TrolleyController()
        {
        }

        [HttpGet]
        [Route("trolleyTotal")]
        public decimal GetTotal(Trolley trolley)
        {


            return 0;
        }
    }
}
