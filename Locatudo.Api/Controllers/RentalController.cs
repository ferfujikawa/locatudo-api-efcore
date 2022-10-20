using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Handlers;
using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Entities.Dtos;
using Locatudo.Domain.Handlers.Commands.Outputs;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/rentals")]
    [Produces("application/json")]
    public class RentalController : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RentalDto>))]
        public IActionResult List([FromServices] IRentalRepository rentalRepository)
        {
            var rentals = rentalRepository.List<RentalDto>();
            return new OkObjectResult(rentals);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RentalDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(
            [FromServices] IRentalRepository rentalRepository,
            [FromRoute] Guid id)
        {
            var rental = rentalRepository.GetById<RentalDto>(id);
            if (rental == null)
                return new NotFoundResult();
            return new OkObjectResult(rental);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateRentalCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<string>))]
        public IActionResult Create(
            [FromServices] CreateRentalHandler handler,
            [FromBody] CreateRentalCommand command)
        {
            var response = handler.Handle(command);
            if (!response.Success)
                return new BadRequestObjectResult(response.Messages);
            return new CreatedResult("", response.Data);
        }

        [HttpPut]
        [Route("approve")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApproveRentalCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<string>))]
        public IActionResult Approve(
            [FromServices] ApproveRentalHandler handler,
            [FromBody] ApproveRentalCommand command)
        {
            var response = handler.Handle(command);
            if (!response.Success)
                return new BadRequestObjectResult(response.Messages);
            return new OkObjectResult(response.Data);
        }

        [HttpPut]
        [Route("cancel")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CancelRentalCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<string>))]
        public IActionResult Cancel(
            [FromServices] CancelRentalHandler handler,
            [FromBody] CancelRentalCommand command)
        {
            var response = handler.Handle(command);
            if (!response.Success)
                return new BadRequestObjectResult(response.Messages);
            return new OkObjectResult(response.Data);
        }

        [HttpPut]
        [Route("disapprove")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CancelRentalCommandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<string>))]
        public IActionResult Disapprove(
            [FromServices] DisapproveRentalHandler handler,
            [FromBody] DisapproveRentalCommand command)
        {
            var response = handler.Handle(command);
            if (!response.Success)
                return new BadRequestObjectResult(response.Messages);
            return new OkObjectResult(response.Data);
        }
    }
}
