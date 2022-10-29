using Locatudo.Domain.Queries.Requests;
using Locatudo.Domain.Queries.Responses;
using Locatudo.Domain.Repositories;
using Locatudo.Shared.Queries.Handlers;
using Locatudo.Shared.Queries.Responses;

namespace Locatudo.Domain.Queries.Handlers
{
    public class GetRentalByIdHandler : IQueryHandler<GetRentalByIdRequest, RentalResponse>
    {
        private readonly IRentalRepository _rentalRepository;

        public GetRentalByIdHandler(IRentalRepository rentalRepository)
        {
            _rentalRepository = rentalRepository;
        }

        public IQueryResponse<RentalResponse> Handle(GetRentalByIdRequest request)
        {
            if (!request.Validate())
                return new GenericQueryHandlerResponse<RentalResponse>(false, null, request.Notifications);

            var response = _rentalRepository.GetById<RentalResponse>(request.Id);
            if (response == null)
                return new GenericQueryHandlerResponse<RentalResponse>(false, null, "Id", "Locação não encontrada");

            return new GenericQueryHandlerResponse<RentalResponse>(
                true,
                response,
                "Sucesso",
                "Locação obtida");
        }
    }
}
