using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/outsourceds")]
    public class OutsourcedController : Controller
    {
        [HttpGet]
        public IActionResult Create([FromServices] IOutsourcedRepository outsourcedRepository)
        {
            var outsourceds = outsourcedRepository.List();
            return new OkObjectResult(outsourceds);
        }
    }
}
