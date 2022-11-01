using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Commands.Validators;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Handlers
{
    public class ChangeEquipmentManagerHandler : ICommandHandler<ChangeEquipmentManagerRequest, ChangeEquipmentManagerData>
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ChangeEquipmentManagerValidator _validator;

        public ChangeEquipmentManagerHandler(
            IEquipmentRepository equipmentRepository,
            IDepartmentRepository departmentRepository,
            ChangeEquipmentManagerValidator validator)
        {
            _equipmentRepository = equipmentRepository;
            _departmentRepository = departmentRepository;
            _validator = validator;
        }

        public ICommandResponse<ChangeEquipmentManagerData> Handle(ChangeEquipmentManagerRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return new GenericCommandHandlerResponse<ChangeEquipmentManagerData>(validationResult.Errors);

            var equipment = _equipmentRepository.GetById(request.EquipmentId);
            if (equipment == null)
                return new GenericCommandHandlerResponse<ChangeEquipmentManagerData>("Equipamento não encontrado");

            var departamento = _departmentRepository.GetById(request.DepartmentId);
            if (departamento == null)
                return new GenericCommandHandlerResponse<ChangeEquipmentManagerData>("Departamento não encontrado");

            equipment.ChangeManager(departamento);
            _equipmentRepository.Update(equipment);

            return new GenericCommandHandlerResponse<ChangeEquipmentManagerData>(
                new ChangeEquipmentManagerData(equipment.Id, equipment.Name, departamento.Id, departamento.Name),
                "Departamento gerenciador do equipamento alterado");
        }
    }
}
