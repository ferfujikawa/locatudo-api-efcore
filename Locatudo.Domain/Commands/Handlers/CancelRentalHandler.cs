using Locatudo.Domain.Commands.Requests;
using Locatudo.Domain.Commands.Responses;
using Locatudo.Domain.Commands.Validators;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Commands.Handlers;
using Locatudo.Shared.Commands.Responses;

namespace Locatudo.Domain.Commands.Handlers
{
    public class CancelRentalHandler : ICommandHandler<CancelRentalRequest, CancelRentalData>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly CancelRentalValidator _validator;

        public CancelRentalHandler(
            IRentalRepository rentalRepository,
            CancelRentalValidator validator)
        {
            _rentalRepository = rentalRepository;
            _validator = validator;
        }

        public ICommandResponse<CancelRentalData> Handle(CancelRentalRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return new GenericCommandHandlerResponse<CancelRentalData>(validationResult.Errors);

            var rental = _rentalRepository.GetById(request.RentalId);
            if (rental == null)
                return new GenericCommandHandlerResponse<CancelRentalData>("RentalId", "Locação não encontrada.");

            if (!rental.Cancel())
                return new GenericCommandHandlerResponse<CancelRentalData>("Status", "A situação atual da locação não permite cancelamento.");

            _rentalRepository.Update(rental);

            return new GenericCommandHandlerResponse<CancelRentalData>(
                new CancelRentalData(rental.Id, rental.Status.Value.ToString()),
                "Sucesso",
                "Locação cancelada.");
        }
    }
}
