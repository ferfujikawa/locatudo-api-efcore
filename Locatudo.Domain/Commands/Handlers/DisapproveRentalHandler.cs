using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Commands.Validators;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Handlers
{
    public class DisapproveRentalHandler : ICommandHandler<DisapproveRentalRequest, DisapproveRentalData>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly DisapproveRentalValidator _validator;

        public DisapproveRentalHandler(
            IRentalRepository rentalRepository,
            IEmployeeRepository employeeRepository,
            DisapproveRentalValidator validator)
        {
            _rentalRepository = rentalRepository;
            _employeeRepository = employeeRepository;
            _validator = validator;
        }

        public ICommandResponse<DisapproveRentalData> Handle(DisapproveRentalRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return new GenericCommandHandlerResponse<DisapproveRentalData>(validationResult.Errors);

            var appraiser = _employeeRepository.GetById(request.AppraiserId);
            if (appraiser == null)
                return new GenericCommandHandlerResponse<DisapproveRentalData>("Funcionário não encontrado.");

            var rental = _rentalRepository.GetByIdIncludingEquipment(request.RentalId);
            if (rental == null)
                return new GenericCommandHandlerResponse<DisapproveRentalData>("Locação não encontrada.");

            if (!rental.CanBeEvaluatedBy(appraiser))
                return new GenericCommandHandlerResponse<DisapproveRentalData>("Aprovador não está lotado no departamento gerenciador do equipamento.");

            if (!rental.Disapprove(appraiser))
                return new GenericCommandHandlerResponse<DisapproveRentalData>("A situação atual da locação não permite reprovação.");

            _rentalRepository.Update(rental);

            return new GenericCommandHandlerResponse<DisapproveRentalData>(
                new DisapproveRentalData(rental.Id, appraiser.Id, appraiser.Name.ToString(), rental.Status.Value.ToString()),
                "Locação reprovada.");
        }
    }
}
