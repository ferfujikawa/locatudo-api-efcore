﻿using Locatudo.Domain.Repositories;
using Locatudo.Domain.Entities;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Handlers
{
    public class CreateEquipmentHandler : ICommandHandler<CreateEquipmentRequest, CreateEquipment>
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public CreateEquipmentHandler(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public ICommandResponse<CreateEquipment> Handle(CreateEquipmentRequest command)
        {
            if (!command.Validate())
                return new GenericCommandHandlerResponse<CreateEquipment>(false, null, command.Notifications);

            var equipment = new Equipment(command.Name);
            _equipmentRepository.Create(equipment);

            return new GenericCommandHandlerResponse<CreateEquipment>(
                true,
                new CreateEquipment(equipment.Id, equipment.Name),
                "Sucesso",
                "Equipament cadastrado");
        }
    }
}