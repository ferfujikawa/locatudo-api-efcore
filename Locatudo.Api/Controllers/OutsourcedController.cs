using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Entities.Dtos;
using Locatudo.Domain.Handlers.Commands.Outputs;
using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Handlers;

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
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IEnumerable<string>))]
        public IActionResult Delete(
            [FromServices] DeleteOutsourcedHandler deleteOutsourcedHandler,
            [FromRoute] Guid id)
        {
            var command = new DeleteOutsourcedCommand(id);
            var response = deleteOutsourcedHandler.Handle(command);

            if (!response.Success)
                return new NotFoundObjectResult(response.Messages);
            return new OkObjectResult(response.Data);
        }
    }
}
