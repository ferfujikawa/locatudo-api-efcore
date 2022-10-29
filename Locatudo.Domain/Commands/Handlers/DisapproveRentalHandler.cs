using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Handlers
{
    public class DisapproveRentalHandler : ICommandHandler<DisapproveRentalRequest, DisapproveRentalData>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DisapproveRentalHandler(IRentalRepository rentalRepository, IEmployeeRepository employeeRepository)
        {
            _rentalRepository = rentalRepository;
            _employeeRepository = employeeRepository;
        }

        public ICommandResponse<DisapproveRentalData> Handle(DisapproveRentalRequest request)
        {
            if (!request.Validate())
                return new GenericCommandHandlerResponse<DisapproveRentalData>(false, null, request.Notifications);

            var appraiser = _employeeRepository.GetById(request.AppraiserId);
            if (appraiser == null)
                return new GenericCommandHandlerResponse<DisapproveRentalData>(false, null, "AppraiserId", "Funcionário não encontrado.");

            var rental = _rentalRepository.GetByIdIncludingEquipment(request.RentalId);
            if (rental == null)
                return new GenericCommandHandlerResponse<DisapproveRentalData>(false, null, "RentalId", "Locação não encontrada.");

            if (!rental.CanBeEvaluatedBy(appraiser))
                return new GenericCommandHandlerResponse<DisapproveRentalData>(false, null, "AppraiserId", "Aprovador não está lotado no departamento gerenciador do equipamento.");

            if (!rental.Disapprove(appraiser))
                return new GenericCommandHandlerResponse<DisapproveRentalData>(false, null, "Status", "A situação atual da locação não permite reprovação.");

            _rentalRepository.Update(rental);

            return new GenericCommandHandlerResponse<DisapproveRentalData>(
                true,
                new DisapproveRentalData(rental.Id, appraiser.Id, appraiser.Name.ToString(), rental.Status.Value.ToString()),
                "Sucesso",
                "Locação reprovada.");
        }
    }
}
