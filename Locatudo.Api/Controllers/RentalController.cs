using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Handlers;
using Locatudo.Domain.Handlers.Commands.Inputs;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/rentals")]
    public class RentalController : Controller
    {
        [HttpGet]
        public IActionResult List([FromServices] IRentalRepository rentalRepository)
        {
            var rentals = rentalRepository.List();
            return new OkObjectResult(rentals);
        }

        [HttpPost]
        public IActionResult Create(
            [FromServices] CreateRentalHandler handler,
            [FromBody] CreateRentalCommand command)
        {
            var response = handler.Handle(command);
            return new OkObjectResult(response);
        }
    }
}
