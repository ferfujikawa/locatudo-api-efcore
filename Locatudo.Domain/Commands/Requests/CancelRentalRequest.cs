using Flunt.Notifications;
using Locatudo.Domain.Commands.Contracts;
using Locatudo.Shared.Commands.Requests;

namespace Locatudo.Domain.Commands.Requests
{
    public class CancelRentalRequest : Notifiable<Notification>, ICommandRequest
    {
        public Guid RentalId { get; set; }

        public CancelRentalRequest()
        {
        }

        public CancelRentalRequest(Guid rentalId)
        {
            RentalId = rentalId;
        }

        public bool Validate()
        {
            AddNotifications(new CancelRentalContract(this));

            return IsValid;
        }
    }
}
