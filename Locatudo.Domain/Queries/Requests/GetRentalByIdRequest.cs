using Locatudo.Shared.Queries.Requests;

namespace Locatudo.Domain.Queries.Requests
{
    public class GetRentalByIdRequest : IQueryRequest
    {
        public Guid Id { get; set; }

        public GetRentalByIdRequest()
        {
        }

        public GetRentalByIdRequest(Guid id)
        {
            Id = id;
        }
    }
}
