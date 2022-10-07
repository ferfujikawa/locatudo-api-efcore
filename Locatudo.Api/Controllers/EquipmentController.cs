using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/equipments")]
    public class EquipmentController : Controller
    {
        [HttpPost]
        public IActionResult Create(
            [FromServices] CreateEquipmentHandler handler,
            [FromBody] CreateEquipmentCommand command
            )
        {
            var response = handler.Handle(command);
            return new OkObjectResult(response);
        }
    }
}
