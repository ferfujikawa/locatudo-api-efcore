using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Entities.Dtos;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/outsourceds")]
    [Produces("application/json")]
    public class OutsourcedController : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OutsourcedDto>))]
        public IActionResult List([FromServices] IOutsourcedRepository outsourcedRepository)
        {
            var outsourceds = outsourcedRepository.List<OutsourcedDto>();
            return new OkObjectResult(outsourceds);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(
            [FromServices] IOutsourcedRepository outsourcedRepository,
            [FromRoute] Guid id)
        {
            outsourcedRepository.Delete(id);
            return new OkResult();
        }
    }
}
