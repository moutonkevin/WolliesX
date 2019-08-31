using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Wollies.Contracts;

namespace Wollies.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public User Get()
        {
            return new User
            {
                Name = _configuration["Name"],
                Token = _configuration["Token"]
            };
        }
    }
}
