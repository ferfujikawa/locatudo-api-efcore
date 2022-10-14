using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/equipments")]
    public class EquipmentController : Controller
    {
        [HttpGet]
        public IActionResult List([FromServices] IEquipmentRepository equipmentRepository)
        {
            var equipments = equipmentRepository.List();
            return new OkObjectResult(equipments);
        }

        [HttpPost]
        public IActionResult Create(
            [FromServices] CreateEquipmentHandler handler,
            [FromBody] CreateEquipmentCommand command)
        {
            var response = handler.Handle(command);
            return new OkObjectResult(response);
        }

        [HttpPut]
        [Route("change_manager")]
        public IActionResult ChangeManager(
            [FromServices] ChangeEquipmentManagerHandler handler,
            [FromBody] ChangeEquipmentManagerCommand command)
        {
            var response = handler.Handle(command);
            return new OkObjectResult(response);
        }
    }
}
