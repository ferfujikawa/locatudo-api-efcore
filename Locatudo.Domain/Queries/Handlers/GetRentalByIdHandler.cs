using Locatudo.Domain.Queries.Requests;
using Locatudo.Domain.Queries.Responses;
using Locatudo.Domain.Queries.Validators;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Queries.Handlers;
using Locatudo.Shared.Queries.Responses;

namespace Locatudo.Domain.Queries.Handlers
{
    public class GetRentalByIdHandler : IQueryHandler<GetRentalByIdRequest, RentalResponse>
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly GetRentalByIdValidator _validator;

        public GetRentalByIdHandler(
            IRentalRepository rentalRepository,
            GetRentalByIdValidator validator)
        {
            _rentalRepository = rentalRepository;
            _validator = validator;
        }

        public IQueryResponse<RentalResponse> Handle(GetRentalByIdRequest request)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
                return new GenericQueryHandlerResponse<RentalResponse>(validationResult.Errors);

            var response = _rentalRepository.GetById<RentalResponse>(request.Id);
            if (response == null)
                return new GenericQueryHandlerResponse<RentalResponse>("Locação não encontrada");

            return new GenericQueryHandlerResponse<RentalResponse>(
                response,
                "Locação obtida");
        }
    }
}
