using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Entities.Dtos;
using Locatudo.Domain.Queries.Commands.Inputs;
using Locatudo.Domain.Queries.Handlers;
using Locatudo.Domain.Queries.Commands.Outputs;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteOutsourcedCommandResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IReadOnlyCollection<string>))]
        public IActionResult Delete(
            [FromServices] DeleteOutsourcedHandler deleteHandler,
            [FromRoute] Guid id)
        {
            var command = new DeleteOutsourcedCommand(id);
            var response = deleteHandler.Handle(command);

            if (!response.Success)
                return new NotFoundObjectResult(response.Messages);
            return new OkObjectResult(response.Data);
        }
    }
}
