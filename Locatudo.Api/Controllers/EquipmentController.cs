using Microsoft.AspNetCore.Mvc;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Entities.Dtos;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Handlers;

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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(IEnumerable<CreateEquipment>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<string>))]
        public IActionResult Create(
            [FromServices] CreateEquipmentHandler handler,
            [FromBody] CreateEquipmentRequest request)
        {
            var response = handler.Handle(request);
            if (!response.Success)
                return new BadRequestObjectResult(response.Messages);
            return new CreatedResult("", response.Data);
        }

        [HttpPut]
        [Route("change_manager")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ChangeEquipmentManagerData>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IReadOnlyCollection<string>))]
        public IActionResult ChangeManager(
            [FromServices] ChangeEquipmentManagerHandler handler,
            [FromBody] ChangeEquipmentManagerRequest request)
        {
            var response = handler.Handle(request);
            if (!response.Success)
                return new BadRequestObjectResult(response.Messages);
            return new OkObjectResult(response.Data);
        }
    }
}
