using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Entities.Dtos;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Commands.Handlers;
using Locatudo.Domain.Commands.Requests;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DeleteOutsourcedData))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(IEnumerable<string>))]
        public IActionResult Delete(
            [FromServices] DeleteOutsourcedHandler deleteOutsourcedHandler,
            [FromRoute] Guid id)
        {
            var request = new DeleteOutsourcedRequest(id);
            var response = deleteOutsourcedHandler.Handle(request);

            if (!response.Success)
                return new NotFoundObjectResult(response.Messages);
            return new OkObjectResult(response.Data);
        }
    }
}
