using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Handlers.Commands.Outputs;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Handlers;
using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Handlers
{
    public class DisapproveRentalHandler : IHandler<DisapproveRentalCommand, DisapproveRentalCommandResponse>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DisapproveRentalHandler(IRentalRepository rentalRepository, IEmployeeRepository employeeRepository)
        {
            _rentalRepository = rentalRepository;
            _employeeRepository = employeeRepository;
        }

        public ICommandResponse<DisapproveRentalCommandResponse> Handle(DisapproveRentalCommand command)
        {
            if (!command.Validate())
                return new GenericHandlerResponse<DisapproveRentalCommandResponse>(false, null, command.Notifications);

            var appraiser = _employeeRepository.GetById(command.AppraiserId);
            if (appraiser == null)
                return new GenericHandlerResponse<DisapproveRentalCommandResponse>(false, null, "AppraiserId", "Funcionário não encontrado.");

            var rental = _rentalRepository.GetById(command.RentalId);
            if (rental == null)
                return new GenericHandlerResponse<DisapproveRentalCommandResponse>(false, null, "RentalId", "Locação não encontrada.");

            if (!rental.CanBeEvaluatedBy(appraiser))
                return new GenericHandlerResponse<DisapproveRentalCommandResponse>(false, null, "AppraiserId", "Appraiser não está lotado no departamento gerenciador do equipamento.");

            if (!rental.Disapprove(appraiser))
                return new GenericHandlerResponse<DisapproveRentalCommandResponse>(false, null, "Status", "A situação atual da locação não permite reprovação.");

            return new GenericHandlerResponse<DisapproveRentalCommandResponse>(
                true,
                new DisapproveRentalCommandResponse(rental.Id, appraiser.Id, appraiser.Name.ToString(), rental.Status.Value.ToString()),
                "Sucesso",
                "Locação reprovada.");
        }
    }
}
