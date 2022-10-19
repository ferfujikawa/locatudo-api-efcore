using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Entities.Dtos;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/outsourceds")]
    public class OutsourcedController : Controller
    {
        [HttpGet]
        public IActionResult List([FromServices] IOutsourcedRepository outsourcedRepository)
        {
            var outsourceds = outsourcedRepository.List<OutsourcedDto>();
            return new OkObjectResult(outsourceds);
        }
    }
}
