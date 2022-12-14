using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Commands.Validators;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Handlers
{
    public class ApproveRentalHandler : ICommandHandler<ApproveRentalRequest, ApproveRentalData>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ApproveRentalValidator _validator;

        public ApproveRentalHandler(
            IRentalRepository rentalRepository,
            IEmployeeRepository employeeRepository,
            ApproveRentalValidator validator)
        {
            _rentalRepository = rentalRepository;
            _employeeRepository = employeeRepository;
            _validator = validator;
        }

        public ICommandResponse<ApproveRentalData> Handle(ApproveRentalRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return new GenericCommandHandlerResponse<ApproveRentalData>(validationResult.Errors);

            var appraiser = _employeeRepository.GetById(request.AppraiserId);
            if (appraiser == null)
                return new GenericCommandHandlerResponse<ApproveRentalData>("Funcionário não encontrado");

            var rental = _rentalRepository.GetByIdIncludingEquipment(request.RentalId);
            if (rental == null)
                return new GenericCommandHandlerResponse<ApproveRentalData>("Locação não encontrada.");

            if (rental.CanBeEvaluatedBy(appraiser) == false)
                return new GenericCommandHandlerResponse<ApproveRentalData>("Aprovador não está lotado no departamento gerenciador do equipamento.");

            if (rental.Approve(appraiser) == false)
                return new GenericCommandHandlerResponse<ApproveRentalData>("A situação atual da locação não permite aprovação.");

            _rentalRepository.Update(rental);

            return new GenericCommandHandlerResponse<ApproveRentalData>(
                new ApproveRentalData(rental.Id, appraiser.Id, appraiser.Name.ToString(), rental.Status.Value.ToString()),
                "Locação aprovada");
        }
    }
}
