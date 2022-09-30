using Locatudo.Domain.Handlers.Commands.Inputs;
using Locatudo.Domain.Handlers.Commands.Outputs;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Handlers;
using Locatudo.Shared.Handlers.Commands.Output;

namespace Locatudo.Domain.Handlers
{
    public class CancelRentalHandler : IHandler<CancelRentalCommand, CancelRentalCommandResponse>
    {
        private readonly IRentalRepository _rentalRepository;

        public CancelRentalHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public ICommandResponse<CancelRentalCommandResponse> Handle(CancelRentalCommand command)
        {
            if (!command.Validate())
                return new GenericHandlerResponse<CancelRentalCommandResponse>(false, null, command.Notifications);

            var rental = _rentalRepository.GetById(command.RentalId);
            if (rental == null)
                return new GenericHandlerResponse<CancelRentalCommandResponse>(false, null, "RentalId", "Locação não encontrada.");

            if (!rental.Cancel())
                return new GenericHandlerResponse<CancelRentalCommandResponse>(false, null, "Status", "A situação atual da locação não permite cancelamento.");

            return new GenericHandlerResponse<CancelRentalCommandResponse>(
                true,
                new CancelRentalCommandResponse(rental.Id, rental.Status.Value.ToString()),
                "Sucesso",
                "Locação cancelada.");
        }
    }
}
