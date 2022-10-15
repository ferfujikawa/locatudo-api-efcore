using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Handlers;
using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Entities.Dtos;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/rentals")]
    public class RentalController : Controller
    {
        [HttpGet]
        public IActionResult List([FromServices] IRentalRepository rentalRepository)
        {
            var rentals = rentalRepository.List<RentalDto>();
            return new OkObjectResult(rentals);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(
            [FromServices] IRentalRepository rentalRepository,
            [FromRoute] Guid id)
        {
            var rental = rentalRepository.GetById(id);
            return new OkObjectResult(rental);
        }

        [HttpPost]
        public IActionResult Create(
            [FromServices] CreateRentalHandler handler,
            [FromBody] CreateRentalCommand command)
        {
            var response = handler.Handle(command);
            return new OkObjectResult(response);
        }

        [HttpPut]
        [Route("approve")]
        public IActionResult Approve(
            [FromServices] ApproveRentalHandler handler,
            [FromBody] ApproveRentalCommand command)
        {
            var response = handler.Handle(command);
            return new OkObjectResult(response);
        }

        [HttpPut]
        [Route("cancel")]
        public IActionResult Cancel(
            [FromServices] CancelRentalHandler handler,
            [FromBody] CancelRentalCommand command)
        {
            var response = handler.Handle(command);
            return new OkObjectResult(response);
        }

        [HttpPut]
        [Route("disapprove")]
        public IActionResult Disapprove(
            [FromServices] DisapproveRentalHandler handler,
            [FromBody] DisapproveRentalCommand command)
        {
            var response = handler.Handle(command);
            return new OkObjectResult(response);
        }
    }
}
