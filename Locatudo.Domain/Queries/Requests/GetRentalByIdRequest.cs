using Flunt.Notifications;
using Locatudo.Domain.Queries.Contracts;
using Locatudo.Shared.Queries.Requests;

namespace Locatudo.Domain.Queries.Requests
{
    public class GetRentalByIdRequest : Notifiable<Notification>, IQueryRequest
    {
        public Guid Id { get; set; }

        public GetRentalByIdRequest()
        {
        }

        public GetRentalByIdRequest(Guid id)
        {
            Id = id;
        }

        public bool Validate()
        {
            AddNotifications(new GetRentalByIdContract(this));

            return IsValid;
        }
    }
}
