using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Handlers;
using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Entities.Dtos;
using Locatudo.Domain.Handlers.Commands.Outputs;

namespace Locatudo.Api.Controllers
{
    [ApiController]
    [Route("v1/equipments")]
    [Produces("application/json")]
    public class EquipmentController : Controller
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EquipmentDto>))]
        public IActionResult List([FromServices] IEquipmentRepository equipmentRepository)
        {
            var equipments = equipmentRepository.List<EquipmentDto>();
            return new OkObjectResult(equipments);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<CreateEquipmentCommandResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<string>))]
        public IActionResult Create(
            [FromServices] CreateEquipmentHandler handler,
            [FromBody] CreateEquipmentCommand command)
        {
            var response = handler.Handle(command);
            if (!response.Success)
                return new BadRequestObjectResult(response.Messages);
            return new CreatedResult("", response.Data);
        }

        [HttpPut]
        [Route("change_manager")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChangeEquipmentManagerCommandResponse>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<string>))]
        public IActionResult ChangeManager(
            [FromServices] ChangeEquipmentManagerHandler handler,
            [FromBody] ChangeEquipmentManagerCommand command)
        {
            var response = handler.Handle(command);
            if (!response.Success)
                return new BadRequestObjectResult(response.Messages);
            return new OkObjectResult(response.Data);
        }
    }
}
