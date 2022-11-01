﻿using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Handlers
{
    public class ChangeEquipmentManagerHandler : ICommandHandler<ChangeEquipmentManagerRequest, ChangeEquipmentManagerData>
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public ChangeEquipmentManagerHandler(IEquipmentRepository equipmentRepository, IDepartmentRepository departmentRepository)
        {
            _equipmentRepository = equipmentRepository;
            _departmentRepository = departmentRepository;
        }

        public ICommandResponse<ChangeEquipmentManagerData> Handle(ChangeEquipmentManagerRequest request)
        {
            if (!request.Validate())
                return new GenericCommandHandlerResponse<ChangeEquipmentManagerData>(false, null, request.Notifications);

            var equipment = _equipmentRepository.GetById(request.EquipmentId);
            if (equipment == null)
                return new GenericCommandHandlerResponse<ChangeEquipmentManagerData>("EquipmentId", "Equipamento não encontrado");

            var departamento = _departmentRepository.GetById(request.DepartmentId);
            if (departamento == null)
                return new GenericCommandHandlerResponse<ChangeEquipmentManagerData>("DepartmentId", "Departamento não encontrado");

            equipment.ChangeManager(departamento);
            _equipmentRepository.Update(equipment);

            return new GenericCommandHandlerResponse<ChangeEquipmentManagerData>(
                new ChangeEquipmentManagerData(equipment.Id, equipment.Name, departamento.Id, departamento.Name),
                "Sucesso",
                "Departamento gerenciador do equipamento alterado");
        }
    }
}
