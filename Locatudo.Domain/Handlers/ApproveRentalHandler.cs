using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Handlers.Commands.Outputs;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Handlers;
using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Handlers
{
    public class ApproveRentalHandler : IHandler<ApproveRentalCommand, ApproveRentalCommandResponse>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public ApproveRentalHandler(IRentalRepository rentalRepository, IEmployeeRepository employeeRepository)
        {
            _rentalRepository = rentalRepository;
            _employeeRepository = employeeRepository;
        }

        public ICommandResponse<ApproveRentalCommandResponse> Handle(ApproveRentalCommand command)
        {
            if (!command.Validate())
                return new GenericHandlerResponse<ApproveRentalCommandResponse>(false, null, command.Notifications);

            var appraiser = _employeeRepository.GetById(command.AppraiserId);
            if (appraiser == null)
                return new GenericHandlerResponse<ApproveRentalCommandResponse>(false, null, "AppraiserId", "Funcionário não encontrado");

            var rental = _rentalRepository.GetByIdIncludingEquipment(command.RentalId);
            if (rental == null)
                return new GenericHandlerResponse<ApproveRentalCommandResponse>(false, null, "RentalId", "Locação não encontrada.");

            if (rental.CanBeEvaluatedBy(appraiser) == false)
                return new GenericHandlerResponse<ApproveRentalCommandResponse>(false, null, "AppraiserId", "Aprovador não está lotado no departamento gerenciador do equipamento.");

            if (rental.Approve(appraiser) == false)
                return new GenericHandlerResponse<ApproveRentalCommandResponse>(false, null, "Status", "A situação atual da locação não permite aprovação.");

            _rentalRepository.Update(rental);

            return new GenericHandlerResponse<ApproveRentalCommandResponse>(
                true,
                new ApproveRentalCommandResponse(rental.Id, appraiser.Id, appraiser.Name.ToString(), rental.Status.Value.ToString()),
                "Sucesso",
                "Locação aprovada");
        }
    }
}
