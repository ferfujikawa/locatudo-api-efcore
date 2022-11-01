using Locatudo.Domain.Repositories;
using Locatudo.Domain.Entities;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Validators;

namespace Locatudo.Domain.Commands.Handlers
{
    public class CreateEquipmentHandler : ICommandHandler<CreateEquipmentRequest, CreateEquipment>
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly CreateEquipmentValidator _validator;

        public CreateEquipmentHandler(
            IEquipmentRepository equipmentRepository,
            CreateEquipmentValidator validator)
        {
            _equipmentRepository = equipmentRepository;
            _validator = validator;
        }

        public ICommandResponse<CreateEquipment> Handle(CreateEquipmentRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return new GenericCommandHandlerResponse<CreateEquipment>(validationResult.Errors);

            var equipment = new Equipment(request.Name);
            _equipmentRepository.Create(equipment);

            return new GenericCommandHandlerResponse<CreateEquipment>(
                new CreateEquipment(equipment.Id, equipment.Name),
                "Equipament cadastrado");
        }
    }
}
