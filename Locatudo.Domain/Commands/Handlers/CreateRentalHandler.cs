using Locatudo.Domain.Repositories;
using Locatudo.Shared.ValueObjects;
using Locatudo.Domain.Entities;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Validators;

namespace Locatudo.Domain.Commands.Handlers
{
    public class CreateRentalHandler : ICommandHandler<CreateRentalRequest, CreateRentalData>
    {
        private readonly IEquipmentRepository _equipmentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly CreateRentalValidator _validator;

        public CreateRentalHandler(
            IEquipmentRepository equipmentRepository,
            IUserRepository userRepository,
            IRentalRepository rentalRepository,
            CreateRentalValidator validator)
        {
            _equipmentRepository = equipmentRepository;
            _userRepository = userRepository;
            _rentalRepository = rentalRepository;
            _validator = validator;
        }

        public ICommandResponse<CreateRentalData> Handle(CreateRentalRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return new GenericCommandHandlerResponse<CreateRentalData>(validationResult.Errors);

            var equipment = _equipmentRepository.GetById(request.EquipmentId);
            var rentalStart = new RentalTime(request.Start);
            if (equipment == null)
                return new GenericCommandHandlerResponse<CreateRentalData>("Equipamento não encontrado");

            var tenant = _userRepository.GetById(request.TenantId);
            if (tenant == null)
                return new GenericCommandHandlerResponse<CreateRentalData>("Usuário não encontrado");

            if (!_rentalRepository.CheckAvailability(request.EquipmentId, rentalStart))
                return new GenericCommandHandlerResponse<CreateRentalData>("Horário de locação indisponível");

            var rental = new Rental(equipment, tenant, rentalStart);
            _rentalRepository.Create(rental);

            return new GenericCommandHandlerResponse<CreateRentalData>(
                new CreateRentalData(
                    rental.Id,
                    equipment.Id,
                    equipment.Name,
                    tenant.Id,
                    tenant.Name.ToString(),
                    rental.Time.Start,
                    rental.Status.Value.ToString()),
                "Locação cadastrada");
        }
    }
}
