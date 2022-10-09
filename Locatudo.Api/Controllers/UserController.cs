using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/users")]
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult List([FromServices] IUserRepository userRepository)
        {
            var users = userRepository.List();
            return new OkObjectResult(users);
        }
    }
}
