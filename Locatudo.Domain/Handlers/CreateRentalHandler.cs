using Locatudo.Shared.Handlers;
using Locatudo.Shared.Handlers.Commands.Output;
using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Handlers.Commands.Outputs;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.ValueObjects;
using Locatudo.Domain.Entities;

namespace Locatudo.Domain.Handlers
{
    public class CreateRentalHandler : IHandler<CreateRentalCommand, CreateRentalCommandResponse>
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

        public ICommandResponse<CreateRentalCommandResponse> Handle(CreateRentalCommand command)
        {
            if (!command.Validate())
                return new GenericHandlerResponse<CreateRentalCommandResponse>(false, null, command.Notifications);

            var equipment = _equipmentRepository.GetById(command.EquipmentId);
            var rentalStart = new RentalTime(command.Start);
            if (equipment == null)
                return new GenericHandlerResponse<CreateRentalCommandResponse>(false, null, "EquipmentId", "Equipamento não encontrado");

            var tenant = _userRepository.GetById(command.TenantId);
            if (tenant == null)
                return new GenericHandlerResponse<CreateRentalCommandResponse>(false, null, "TenantId", "Usuário não encontrado");

            if (!_rentalRepository.CheckAvailability(command.EquipmentId, rentalStart))
                return new GenericHandlerResponse<CreateRentalCommandResponse>(false, null, "Start", "Horário de locação indisponível");

            var rental = new Rental(equipment, tenant, rentalStart);
            _rentalRepository.Create(rental);

            return new GenericHandlerResponse<CreateRentalCommandResponse>(
                true,
                new CreateRentalCommandResponse(
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
