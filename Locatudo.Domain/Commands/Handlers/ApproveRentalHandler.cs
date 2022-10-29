using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Handlers
{
    public class ApproveRentalHandler : ICommandHandler<ApproveRentalRequest, ApproveRentalData>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ApproveRentalHandler(IRentalRepository rentalRepository, IEmployeeRepository employeeRepository)
        {
            _rentalRepository = rentalRepository;
            _employeeRepository = employeeRepository;
        }

        public ICommandResponse<ApproveRentalData> Handle(ApproveRentalRequest request)
        {
            if (!request.Validate())
                return new GenericCommandHandlerResponse<ApproveRentalData>(false, null, request.Notifications);

            var appraiser = _employeeRepository.GetById(request.AppraiserId);
            if (appraiser == null)
                return new GenericCommandHandlerResponse<ApproveRentalData>(false, null, "AppraiserId", "Funcionário não encontrado");

            var rental = _rentalRepository.GetByIdIncludingEquipment(request.RentalId);
            if (rental == null)
                return new GenericCommandHandlerResponse<ApproveRentalData>(false, null, "RentalId", "Locação não encontrada.");

            if (rental.CanBeEvaluatedBy(appraiser) == false)
                return new GenericCommandHandlerResponse<ApproveRentalData>(false, null, "AppraiserId", "Aprovador não está lotado no departamento gerenciador do equipamento.");

            if (rental.Approve(appraiser) == false)
                return new GenericCommandHandlerResponse<ApproveRentalData>(false, null, "Status", "A situação atual da locação não permite aprovação.");

            _rentalRepository.Update(rental);

            return new GenericCommandHandlerResponse<ApproveRentalData>(
                true,
                new ApproveRentalData(rental.Id, appraiser.Id, appraiser.Name.ToString(), rental.Status.Value.ToString()),
                "Sucesso",
                "Locação aprovada");
        }
    }
}
