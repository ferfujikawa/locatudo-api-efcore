using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Handlers.Commands.Outputs;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Handlers;
using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Handlers
{
    public class ChangeEquipmentManagerHandler : IHandler<ChangeEquipmentManagerCommand, ChangeEquipmentManagerCommandResponse>
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public ChangeEquipmentManagerHandler(IEquipmentRepository equipmentRepository, IDepartmentRepository departmentRepository)
        {
            _equipmentRepository = equipmentRepository;
            _departmentRepository = departmentRepository;
        }

        public ICommandResponse<ChangeEquipmentManagerCommandResponse> Handle(ChangeEquipmentManagerCommand command)
        {
            if (!command.Validate())
                return new GenericHandlerResponse<ChangeEquipmentManagerCommandResponse>(false, null, command.Notifications);

            var equipment = _equipmentRepository.GetById(command.EquipmentId);
            if (equipment == null)
                return new GenericHandlerResponse<ChangeEquipmentManagerCommandResponse>(false, null, "EquipmentId", "Equipamento não encontrado");

            var departamento = _departmentRepository.GetById(command.DepartmentId);
            if (departamento == null)
                return new GenericHandlerResponse<ChangeEquipmentManagerCommandResponse>(false, null, "DepartmentId", "Departamento não encontrado");

            equipment.ChangeManager(departamento);
            _equipmentRepository.Update(equipment);

            return new GenericHandlerResponse<ChangeEquipmentManagerCommandResponse>(
                true,
                new ChangeEquipmentManagerCommandResponse(equipment.Id, equipment.Name, departamento.Id, departamento.Name),
                "Sucesso",
                "Departamento gerenciador do equipamento alterado");
        }
    }
}
