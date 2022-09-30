using Locatudo.Shared.Handlers;
using Locatudo.Shared.Handlers.Commands.Output;
using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Handlers.Commands.Outputs;
using Locatudo.Domain.Repositories;
using Locatudo.Domain.Entities;

namespace Locatudo.Domain.Handlers
{
    public class CreateEquipmentHandler : IHandler<CreateEquipmentCommand, CreateEquipmentCommandResponse>
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public CreateEquipmentHandler(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public ICommandResponse<CreateEquipmentCommandResponse> Handle(CreateEquipmentCommand command)
        {
            if (!command.Validate())
                return new GenericHandlerResponse<CreateEquipmentCommandResponse>(false, null, command.Notifications);

            var equipment = new Equipment(command.Name);
            _equipmentRepository.Create(equipment);

            return new GenericHandlerResponse<CreateEquipmentCommandResponse>(
                true,
                new CreateEquipmentCommandResponse(equipment.Id, equipment.Name),
                "Sucesso",
                "Equipament cadastrado");
        }
    }
}
