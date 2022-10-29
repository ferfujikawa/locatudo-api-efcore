using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Queries.Handlers;
using Locatudo.Domain.Queries.Responses;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Handlers;
using Locatudo.Domain.Queries.Requests;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/rentals")]
    [Produces("application/json")]
    public class RentalController : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RentalResponse>))]
        public IActionResult List([FromServices] IRentalRepository rentalRepository)
        {
            var rentals = rentalRepository.List<RentalResponse>();
            return new OkObjectResult(rentals);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RentalResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(
            [FromServices] GetRentalByIdHandler handler,
            [FromRoute] Guid id)
        {
            var command = new GetRentalByIdRequest(id);
            var response = handler.Handle(command);
            if (!response.Success)
                return new NotFoundResult();
            return new OkObjectResult(response.Data);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateRentalData))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<string>))]
        public IActionResult Create(
            [FromServices] CreateRentalHandler handler,
            [FromBody] CreateRentalRequest command)
        {
            var response = handler.Handle(command);
            if (!response.Success)
                return new BadRequestObjectResult(response.Messages);
            return new CreatedResult("", response.Data);
        }

        [HttpPut]
        [Route("approve")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApproveRentalData))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<string>))]
        public IActionResult Approve(
            [FromServices] ApproveRentalHandler handler,
            [FromBody] ApproveRentalRequest command)
        {
            var response = handler.Handle(command);
            if (!response.Success)
                return new BadRequestObjectResult(response.Messages);
            return new OkObjectResult(response.Data);
        }

        [HttpPut]
        [Route("cancel")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CancelRentalData))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<string>))]
        public IActionResult Cancel(
            [FromServices] CancelRentalHandler handler,
            [FromBody] CancelRentalRequest command)
        {
            var response = handler.Handle(command);
            if (!response.Success)
                return new BadRequestObjectResult(response.Messages);
            return new OkObjectResult(response.Data);
        }

        [HttpPut]
        [Route("disapprove")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CancelRentalData))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<string>))]
        public IActionResult Disapprove(
            [FromServices] DisapproveRentalHandler handler,
            [FromBody] DisapproveRentalRequest command)
        {
            var response = handler.Handle(command);
            if (!response.Success)
                return new BadRequestObjectResult(response.Messages);
            return new OkObjectResult(response.Data);
        }
    }
}
