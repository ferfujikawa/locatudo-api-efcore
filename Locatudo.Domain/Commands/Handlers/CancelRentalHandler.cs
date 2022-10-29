using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Handlers
{
    public class CancelRentalHandler : ICommandHandler<CancelRentalRequest, CancelRentalData>
    {
        private readonly IRentalRepository _rentalRepository;

        public CancelRentalHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public ICommandResponse<CancelRentalData> Handle(CancelRentalRequest command)
        {
            if (!command.Validate())
                return new GenericCommandHandlerResponse<CancelRentalData>(false, null, command.Notifications);

            var rental = _rentalRepository.GetById(command.RentalId);
            if (rental == null)
                return new GenericCommandHandlerResponse<CancelRentalData>(false, null, "RentalId", "Locação não encontrada.");

            if (!rental.Cancel())
                return new GenericCommandHandlerResponse<CancelRentalData>(false, null, "Status", "A situação atual da locação não permite cancelamento.");

            _rentalRepository.Update(rental);

            return new GenericCommandHandlerResponse<CancelRentalData>(
                true,
                new CancelRentalData(rental.Id, rental.Status.Value.ToString()),
                "Sucesso",
                "Locação cancelada.");
        }
    }
}
