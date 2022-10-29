using Locatudo.Domain.Repositories;
using Locatudo.Shared.ValueObjects;
using Locatudo.Domain.Entities;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Commands.Requests;

namespace Locatudo.Domain.Commands.Handlers
{
    public class CreateRentalHandler : ICommandHandler<CreateRentalRequest, CreateRentalData>
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRentalRepository _rentalRepository;

        public CreateRentalHandler(IEquipmentRepository equipmentRepository, IUserRepository userRepository, IRentalRepository rentalRepository)
        {
            _equipmentRepository = equipmentRepository;
            _userRepository = userRepository;
            _rentalRepository = rentalRepository;
        }

        public ICommandResponse<CreateRentalData> Handle(CreateRentalRequest command)
        {
            if (!command.Validate())
                return new GenericCommandHandlerResponse<CreateRentalData>(false, null, command.Notifications);

            var equipment = _equipmentRepository.GetById(command.EquipmentId);
            var rentalStart = new RentalTime(command.Start);
            if (equipment == null)
                return new GenericCommandHandlerResponse<CreateRentalData>(false, null, "EquipmentId", "Equipamento não encontrado");

            var tenant = _userRepository.GetById(command.TenantId);
            if (tenant == null)
                return new GenericCommandHandlerResponse<CreateRentalData>(false, null, "TenantId", "Usuário não encontrado");

            if (!_rentalRepository.CheckAvailability(command.EquipmentId, rentalStart))
                return new GenericCommandHandlerResponse<CreateRentalData>(false, null, "Start", "Horário de locação indisponível");

            var rental = new Rental(equipment, tenant, rentalStart);
            _rentalRepository.Create(rental);

            return new GenericCommandHandlerResponse<CreateRentalData>(
                true,
                new CreateRentalData(
                    rental.Id,
                    equipment.Id,
                    equipment.Name,
                    tenant.Id,
                    tenant.Name.ToString(),
                    rental.Time.Start,
                    rental.Status.Value.ToString()),
                "Sucesso",
                "Locação cadastrada");
        }
    }
}
